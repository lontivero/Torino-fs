# Day 1

## The project

I didn't find any project tiny enough to be rewritten in F# with my current limited understanding of the language so, I decided to translate one of my own toy projects, [Torino](https://github.com/lontivero/Torino) (a Tor control port library for .NET), because:

1. it is really small and simple,
2. it can be useful for some people,
3. it requires solving a few interesting problems and,
4. because I wrote it in about 5 days and the I known how much I will invest on learning.

## Achivements

I was able to implement, at least partialy, the Tor process launcher which is the one that starts the Tor process with the parameters that we provide. The API loos like this:

```f#
let torProcessResult = TorStartInfo.create ()
                    |> TorStartInfo.configFile "./tor.rc"
                    |> TorStartInfo.extraConfig (Map.ofList [
                        "--SocksPort", "9050"
                        "--ControlPort", "9051"
                        "--CookieAuthFile", "./cookie-auth"
                        "--CookieAuthentication", "1"
                        ])
                    |> Launcher.launch
                    |> Async.RunSynchronously
```


## Experience

Even for a google-oriented programming style this start felt a bit dissaponting because I spent almost one day trying to understand what's the idiomatic way to write a piece of code in funtional style and I ended with something very similar to the original c# code, in fact this one is a bit more verbose.

Sadly I don't know how to test this code because it is impure. Another frsutrating thing is that I didn't find a way to collect errors in the `Process.OutputDataReceived` event handler and I had to mutate a `List<string>`.

It seems there are multiple ways to do async stuff in F# what is a bit confusing but given this code is just a toy then it doesn't need anything too performant so, who cares (at least for now).

## What I learnt

First, or well this piece of code is not the best to learn F# or I stll don't know how to program (i bet my hat this is the real reason). How to program against an interface instead of doing it against the real process without going down the IO Monad rabbit hole (I am referencing to this video https://www.youtube.com/watch?v=h00DRlHewrM) is something I need to learn still.

I didn't notice a big difference with c# code at this point except in the fact that the developoment flow is different, imo writing code in an interactive mode is better even when I still don't master it (I think). But anyway, I finished the first version of this code and it just worked.

I spent another hour or so trying to understand how to organize the code in modules, submodules, namespaces and so on. I don't understand what namespaces are good for in F# honestly, I was just forced to add one by the compiler and forced to remove ti by the REPL (makes no sense to me, yet?) Ohh btw, I still have no idea how to do it.

Another things that I didn't solved is the visibility of the modules, functions and types. In c# we have `private`, `public`, `internal` and `protected` keywords, but I belive in F# (and OCaml btw) they use `.fsi` and `mli` files for that. I have to read more.

While in F# devs use a OOP style without feeling guilty of any crime, in OCaml they don't use it or at least I didn't find projects that use it and I don't understand why (could it be because F# interops with c#?)

I lernt how to use `Result` but my first impulse was to catch exceptions and and return them wrapped in the `Error`.

I learnt that the type inference system is magical except when it have no idea what you are trying to do so, from time to time I needed to help it to help me. This is by anotating the types or some times just by swaping two lines of code and placing first the line that has more info for the type system (this sound crazy but it is true, and not so bad I think).

## Next steps

Implement the `TorController` module. That's the biggest part (99% of the code I would say).
