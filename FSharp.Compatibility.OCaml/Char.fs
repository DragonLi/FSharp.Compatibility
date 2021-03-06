﻿(*

Copyright 2005-2009 Microsoft Corporation
Copyright 2012 Jack Pappas

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

*)

// References:
// http://caml.inria.fr/pub/docs/manual-ocaml/libref/Char.html

/// Character operations.
[<CompilerMessage(
    "This module is for ML compatibility. \
    This message can be disabled using '--nowarn:62' or '#nowarn \"62\"'.",
    62, IsHidden = true)>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module FSharp.Compatibility.OCaml.Char

open System

/// Return the ASCII code of the argument.
let code (c : char) : int =
    // NOTE : The OCaml documentation for this function does not specify what to do
    // if the character is a non-ASCII (i.e., Unicode) character; until the specific
    // behavior of the function can be determined, we'll raise an exception here,
    // as done in the 'Char.chr' function.
    if int c > int Byte.MaxValue then
        raise <| Invalid_argument "Char.code"
    
    int c

/// Return the character with the given ASCII code.
let chr (value : int) : char =
    if value < int Byte.MinValue ||
        value > int Byte.MaxValue then
        raise <| Invalid_argument "Char.chr"

    char value

/// Return a string representing the given character, with special
/// characters escaped following the lexical conventions of OCaml.
let escaped = function
    (* Special characters are converted to a string and prefixed with a backslash. *)
    | '\u0000'
    | '\a'
    | '\b'
    | '\t'
    | '\n'
    | '\v'
    | '\f'
    | '\r'
    | '\u001a'
    | '\u0022'
    | '\u0027'
    | '\u005c'
    | '\u0060'
        as c ->
        String [| '\\'; c; |] : string

    (* Non-special characters are converted directly to a string. *)
    | c ->
        string c

/// Convert the given character to its equivalent lowercase character.
let inline lowercase (c : char) : char =
    Char.ToLowerInvariant c

/// Convert the given character to its equivalent uppercase character.
let inline uppercase (c : char) : char =
    Char.ToUpperInvariant c

/// An alias for the type of characters.
type t = char

/// The comparison function for characters, with the same specification as <see cref="compare"/>.
let inline compare (a : char) (b : char) =
    compare a b

