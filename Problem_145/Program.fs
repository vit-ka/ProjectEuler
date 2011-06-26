let rec cutNumberToDigitsInReverseOrder (x: int) : int list =
    match x with
    | 0 -> []
    | x -> 
        let xDiv10 = x / 10
        let xMod10 = x % 10
        xMod10 :: cutNumberToDigitsInReverseOrder xDiv10
    

let rec joinDigitsToNumber (l: int list) : int =
    match l with
    | [] -> 0
    | x::xs -> x + (joinDigitsToNumber xs) * 10

let reverseNumber (x : int) : int =
    let reversedDigits = List.rev (cutNumberToDigitsInReverseOrder x)
    let reversedNumber = joinDigitsToNumber reversedDigits
    reversedNumber

let isNumberConsistsOfOnlyFromOddDigits (x: int) : bool =
    let digits = cutNumberToDigitsInReverseOrder x
    let isEveryDigitOdd = List.tryFind (fun x -> x = 0) (List.map (fun x -> x % 2) digits)
    match isEveryDigitOdd with
    |   Some(_) -> false
    |   None -> true

let isReversibleNumber (x : int) : bool =
    let reversedX = reverseNumber x
    let lenOfSourse = (int)(System.Math.Log10((float)x))
    let lenOfReversed = (int)(System.Math.Log10((float)reversedX))
    
    match (lenOfSourse - lenOfReversed, x) with
    |   (0, 0) -> false
    |   (0, x) ->
        let sumOfNumbers = reversedX + x
        isNumberConsistsOfOnlyFromOddDigits sumOfNumbers
    |   (_, _) -> 
        false

let getReversibleNumbersCountFromNToM n m =
    printfn "Starting evaluation at the range [%A;%A)" n m
    List.length (List.filter isReversibleNumber [n .. m - 1])


let getReversibleNumbersCountFromNToMAsync n m =
    async {return (getReversibleNumbersCountFromNToM n m)}

let getReversibleNumbersCountUnderN n step =
    seq [1 .. n / step] |> 
        Seq.map (fun x -> getReversibleNumbersCountFromNToMAsync ((x - 1) * step) (x * step)) |>
        Async.Parallel |>
        Async.RunSynchronously |>
        Seq.sum 

let n = 1000000000
printfn "Reversible numbers count under %A: %A" n (getReversibleNumbersCountUnderN n 1000000)