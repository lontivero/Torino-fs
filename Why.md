The purpose of this document is keep track of my learning process while I learn F#.

# Introduction

My first programming language was `Basic`, `Sinclair BASIC` first and `GW-BASIC` a few months later,
then `DBase III+`, then `Clipper Summer 87`, then `Clipper 5`, `Pascal 4.5`, `FoxPro`, `Microsoft Macro Assembler`,
`C`, `C++`, `Delphi`, `Builder C++`, `Visual Basic`, and many more until `C#`.

Nowadays there are thousands and thousands of programming languages but only a few concentrate the huge majority of
programmers. However, something is clear: programming languages don't last forever or are not cool forever. Whoever
have had the opportunity to meet a Cobol programmer also knows how sad is to be stuck to ancient technologies.

"if you want to know what a C# developer will look like when he’s older look at his Java developer friend" (the joke
is that c# developer don't have java friends). Java is getting older and what looked and felt as sci-fi during the 90's
today is not cool anymore and it is expected imo that new devs will opt for Kotlin, Scala or something else but only
those without a real pasion and that are looking for a job for the eternity will learn Java in the future.

While C# is not in the same situation, because the languaje is light years from Java, the thing is that the languaje is
getting old and is keep updated with more and more features taken from the functional programming world but with
a marketing carefully design to hide that fact because many people believe, specially in Microsoft, that C# developers
are idiots.

## Quick C# history

Lets see, C# 1.x was basically an Improved Java with many many brain farts and restricted to run on Windows only,
because the less platforms you can target, the better (?). It was C# 2.0 that came with `Generics`, `Iterators`,
`Nullable`, `Anonymous methods` and limited support for `Covariance and contravariance` needed to support generics
in a more natural way. All these features are "taken" from functional languajes.

But then C# 3.0 came with Anonymous types `LINQ`, `Lambda expressions`, `Object and collection initializers`,
`Extension methods` and by far my favourite and most clever idea in the world: `Implicitly typed local variables`.
Again, all taken from functional languajes.
> C# 3.0 had begun to lay the groundwork for turning C# into a hybrid Object-Oriented / Functional language.
> Microsoft

Lets forget about the forgetable C# 4, except for `Generic covariant and contravariant`. Clearly Microsoft built
this features for internal usage but decided to bundle it and sell it to us as a new great version.

C# 5.0 bring asynchronous with continuations (a functional concept by the way)

C# 6.0 improved it by introducing async/await syntactic sugar around the ugly `ContinueWith` and a state machine.
This time taken from F#

C# 7.x came with `Static imports` (F#), `Exception filters` (F#), `Expression bodied members` (F# inspired)
and `Null propagator` (Option monad bind in F#??), `Tuples and deconstruction` (F# and most functional languajes),
`Pattern matching` (F# and most functional languajes), `Local functions` (F# and most functional languajes) and
`Discards` (F# and most functional languajes)

C# 8.0 and 9.0 more and more and more pattern matchings, `switch` (all functional languages), more discards, more
anonymous types, more and more functional features. Ahh and `Records` (functional again).


## Some trends in programming languages

Java was designed to prevent mistakes that were common in C++, and it was as far as removing support for `goto` statements. The preprocessor was a cause of mistakes so removed! the goto statement facilitates spaghetti code so removed! the pointers arithment... removed! and so on, that's why it is safer and more productive to develop in Java than it is in C++.

There are many situations where those removed features make sense but in the aggregated ecosystem their removal was the correct thing to do.

What are the current sources of mistakes in C#? `Nulls`, `Shared and mutable state`, `Exceptions handling`, `Improper handing of collections` so, what if we remove all these things? That's what many languages do, many new languages are immutable by default, that doesn't mean we cannot mutate the value of variables but that we have to make them mutable explicitelly otherwise they are immutable. There are languages were `null` is not a thing, we cannot express the concept of `null` and then the NullReferenceException/NullPointerException is not possible anymore. Removing, or limiting, `Exceptions` goes also in the same direction of removing the source of the problem.

## Why F#?

Basically because Haskell is not good for me. I mean, I was studying Haskell for a couple of months, reading everything out there, solving puzzles, understanding a whole stack of new concepts but the language is complex, because in other to use it effectively you need to understand many things that are completelly new for a developer like me. Also, the libraries are a bit messy, some are not very well maintained, the support for concurrent programming is a bit limited imo and the compiler is extensible so it makes you feel that you will never be able to master it. Anyway, I can try again in the future ;)

F# on the other hand is part of the .NET SDK and I know that thing pretty well, the language is "functional first" and comes from OCaml so, it is a language that makes it easy to write functional code but at the same time allows you to do whatever you want. Do you want to write object oriented style code?, you can. Do you want to use loops? you can. Do you want to mutate state? you can.

Also, C# can take things from F# but it will never be F#. Writing funtional code in C#, if well is possible, looks and feels very unnatural.

Finally, F# is a wonderful language to work as an scripting language because you only need to open the terminal to start playing with .NET code. There is no easiest way to do things in .NET. Look: https://i2.paste.pics/35410b697a4b5971317ff125a7a2aebd.png?trs=e715dcd4371e4ecac759bd658524cc97bc912e461c006d696c5078905815d9b3

## A test project

I need to write a tiny project in F# to know how it really feels and what are the painful points. Also, given this will be my first google driven project, it should be something easy. Any idea?



