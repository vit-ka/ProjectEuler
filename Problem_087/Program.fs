// Learn more about F# at http://fsharp.net

let modulus x n =
    int (int x % int n)

let isPrime (n : int) =
    let candidatesToDevisitors = [2 .. int (System.Math.Sqrt(float n))]
    let divisions = List.map (fun (x : int) -> modulus n x) candidatesToDevisitors
    match List.tryFind (fun x -> x = 0) divisions with
    | Some(x) -> false
    | None -> true

let generatePrimeList =
    let numbers = Seq.initInfinite (fun x -> x + 2)
    Seq.filter isPrime numbers

let printAnswerTo maxSum =
    let numbers = generatePrimeList
    
    let tuples = seq {for a in (Seq.takeWhile (fun x -> x * x < maxSum) numbers) do
                        for b in (Seq.takeWhile (fun x -> a * a + x * x * x < maxSum) numbers) do
                            for c in (Seq.takeWhile (fun x -> a * a + b * b * b + x * x * x * x < maxSum) numbers) -> (a,b,c)}
    let answers = Seq.map (fun (a,b,c) -> a * a + b * b * b + c * c * c * c) tuples

    printfn "%A" tuples
    answers

let ans = printAnswerTo 50000000
let uniqueAns = ans |> Set.ofSeq |> Set.toSeq
printfn "%A" uniqueAns
printfn "%A" (Seq.length uniqueAns)
