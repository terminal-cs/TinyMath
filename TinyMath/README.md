# Introduction

This is TinyMath! It is a bare-bones simple and mostly pure math evaluator for C#!

TinyMath allows you to solve basic equations from string input that uses addition, subtraction, multiplication, division, powers, and modulus operators, and has full support for doubles!
Currently parenthasis aren't supported, but they may be added at a later date.

# How to use

Add the TinyMath nuget package (or reference a DLL from the releases tab) to your project. Then, include it in whatever file you want like so:
```cs
using TinyMath;
```
To solve an expression, type the following:
```cs
string ExampleExpression = "2 + 6 / 5 ^ 2 * 8";
SyntaxParser.Evaluate(ExampleExpression);
```
> (Note here, it is assumed you followed the previous step, which is including it into the file.)
And, the result should be returned as a double number! You can store it in a double for later or print it to the console.

# Aditional info

All types of contributions are welcome! Feel free to open issues or submit pull requests!

TinyMath is licensed under the GPL V2 licence.