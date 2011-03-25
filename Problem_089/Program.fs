open System.IO

let parseRomanDigit (ch : char) : int =
    match ch with
    | 'I' -> 1
    | 'V' -> 5
    | 'X' -> 10
    | 'L' -> 50
    | 'C' -> 100
    | 'D' -> 500
    | 'M' -> 1000
    | _ -> 0

let rec parseRomanNumber (literal : char list) : int =
    match literal with
    | [] -> 0
    | head::headNext::tail when parseRomanDigit head < parseRomanDigit headNext  -> 
            parseRomanDigit headNext - parseRomanDigit head + parseRomanNumber tail
    | head::tail -> parseRomanDigit head + parseRomanNumber tail

let rec writeRomanInMinimalForm (roman : int) : char list =
    match roman with
    | a when a >= 1000          -> 'M' :: writeRomanInMinimalForm (a - 1000)
    | a when a >= 900           -> 'C' :: 'M' :: writeRomanInMinimalForm (a - 900)
    | a when a >= 500           -> 'D' :: writeRomanInMinimalForm (a - 500)
    | a when a >= 400           -> 'C' :: 'D' :: writeRomanInMinimalForm (a - 400)
    | a when a >= 100           -> 'C' :: writeRomanInMinimalForm (a - 100)
    | a when a >= 90            -> 'X' :: 'C' :: writeRomanInMinimalForm (a - 90)
    | a when a >= 50            -> 'L' :: writeRomanInMinimalForm (a - 50)
    | a when a >= 40            -> 'X' :: 'L' :: writeRomanInMinimalForm (a - 40)
    | a when a >= 10            -> 'X' :: writeRomanInMinimalForm (a - 10)
    | a when a =  9             -> 'I' :: 'X' :: writeRomanInMinimalForm (a - 9)
    | a when a >= 5             -> 'V' :: writeRomanInMinimalForm (a - 5)
    | a when a =  4             -> 'I' :: 'V' :: writeRomanInMinimalForm (a - 4)
    | a when a >= 1             -> 'I' :: writeRomanInMinimalForm (a - 1)
    | _ -> []

let stringToCharList (str : string) : char list =
   Array.toList(str.ToCharArray())

let charListToString (l : char list) : string =
   new string(List.toArray l)

let fileContent = File.ReadAllLines(@"roman.txt")
let fileContentAsList = Array.toList fileContent

let romanNumbersAsLiterals = List.map stringToCharList fileContentAsList
let romanNumbers = List.map parseRomanNumber romanNumbersAsLiterals
let minimalizedRomanNumbersAsList = List.map writeRomanInMinimalForm romanNumbers

let originalAndMinimizedNumbersLengths = List.zip (List.map (List.length) romanNumbersAsLiterals) ( List.map (List.length) minimalizedRomanNumbersAsList)
let differenceBetweenOriginalAndMinimizedNumbersLengths = List.map (fun (x, y) -> x - y) originalAndMinimizedNumbersLengths

let totalDifference = List.reduce (fun accummulator element -> accummulator + element) differenceBetweenOriginalAndMinimizedNumbersLengths

printfn "totalDifference is %A" totalDifference