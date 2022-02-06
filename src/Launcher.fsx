#if INTERACTIVE
#r "nuget:FSharp.Control.AsyncSeq"
#else
namespace Torino
#endif

open System
open System.IO
open FSharp.Control

module TorStartInfo =
    open System.Runtime.InteropServices

    type T = {
        path : string
        configFile : string option
        extraConfig : Map<string, string>
        onBoostrapping: int -> unit
    }

    let getTorFileName () =
        if RuntimeInformation.IsOSPlatform(OSPlatform.Windows) then "tor.exe"
        elif RuntimeInformation.IsOSPlatform(OSPlatform.OSX) then "tor.real"
        elif RuntimeInformation.IsOSPlatform(OSPlatform.Linux) then "tor"
        else failwith "Unsupported platform."

    let create () =
        let datadir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName())
        let getDataDirRandomFileName fileName =
            Path.Combine(datadir, fileName)
        {
            path = getTorFileName();
            configFile = None;
            extraConfig = Map.ofList [
                "--ControlPort", "auto"
                "--SocksPort", "auto"
                "--CookieAuthentication", "1"
                "--CookieAuthFile", getDataDirRandomFileName("cookie-auth")
                "--ControlPortWriteToFile", getDataDirRandomFileName("control-port")
                ];
            onBoostrapping = fun _ -> ()
        }

    let torFilePath path torStartInfo = { torStartInfo with path = path }
    let configFile configFilePath torStartInfo = { torStartInfo with configFile = Some(configFilePath) }
    let onProgressCallback cb torStartInfo = { torStartInfo with onBoostrapping = cb }
    let extraConfig extras torStartInfo =
        let mergedConfig = Map.fold (fun acc k v -> Map.add k v acc) torStartInfo.extraConfig extras
        { torStartInfo with extraConfig = mergedConfig }

    let buildCmdLineArgts torStartInfo =
        let extras =
            torStartInfo.extraConfig
            |> Map.toList
            |> List.map (fun (k, v) -> $"{k} {v}")
            |> String.concat " "
        let configFilePath =
            torStartInfo.configFile
            |> Option.map (fun cfgFilePath -> $"-f {cfgFilePath}")
            |> Option.defaultValue ""
        $"{configFilePath} {extras}"

module Launcher =
    open System.Diagnostics
    open System.Text.RegularExpressions
    open System.Threading.Tasks
    open System.Collections.Generic

    let launch torStartInfo = async {
        let procStartInfo = ProcessStartInfo(
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            Arguments = TorStartInfo.buildCmdLineArgts torStartInfo,
            FileName = torStartInfo.path
            )

        let tcs = TaskCompletionSource<Result<int, string>>();

        let torProcess = new Process()
        let createEventHandler () =
            let errors = new List<string>()
            let bootstrapLineRegEx = Regex ("Bootstrapped ([0-9]+)%", RegexOptions.Compiled);
            let problemLineRegEx = Regex ("\\[(warn|err)\\] (.*)$", RegexOptions.Compiled);
            let processOutput (evntArg: DataReceivedEventArgs) =
                let line = evntArg.Data + ""
                let bootstrapMatch = bootstrapLineRegEx.Match line
                let problemMatch = problemLineRegEx.Match line

                if bootstrapMatch.Success then
                    let progress = bootstrapMatch.Groups[1].Value |> int
                    //onBootstraping progress
                    if progress = 100 then
                        tcs.SetResult(Ok 100)
                elif problemMatch.Success then
                    let level = problemMatch.Groups[1].Value
                    let msg = problemMatch.Groups[2].Value
                    errors.Add(line)
                    if msg.Contains("see warnings above") then
                        Error (String.concat "\n" errors) |> tcs.SetResult
            processOutput
        let outputDataSubscription = torProcess.OutputDataReceived.Subscribe (
            createEventHandler ()
            )
        let rec exitSubscription : IDisposable = torProcess.Exited.Subscribe (
            fun _ ->
                outputDataSubscription.Dispose()
                exitSubscription.Dispose()
                torProcess.Dispose()
                tcs.SetResult (Ok -1)
            )

        torProcess.StartInfo <- procStartInfo
        try
            torProcess.Start() |> ignore
            torProcess.BeginOutputReadLine()
            let! startResult = tcs.Task |> Async.AwaitTask
            return (startResult |> Result.bind (fun x -> Ok torProcess))
        with
            | :? System.ComponentModel.Win32Exception as e ->
                torProcess.Dispose()
                return Error e.Message
    }

    let launchDemo () =
        TorStartInfo.create ()
        |> TorStartInfo.onProgressCallback (printfn "Bootstrapping %d%%")
        |> launch


Launcher.launchDemo ()
|> Async.RunSynchronously
|> function
    | Ok torProcess -> printfn "has exited: %A" torProcess
    | Error msg -> printfn "%s" msg