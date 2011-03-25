open System.Collections.Generic
open System

let memoize f =
    let cache = Dictionary()
    fun x ->
        try
            if cache.ContainsKey(x) then
                cache.[x]
            else
                let res = f x
                cache.[x] <- res

                if cache.Count % 10000 = 0 then
                    printfn "Cache size is %A" cache.Count
                res
        with 
            | ex ->
                let res = f x
                res        


let rec memoizedGetDelimiters =
    let rec getDelimeters x =
        let candidates = [2 .. (int (Math.Sqrt(float x)))]
        List.filter (fun y -> x % y = 0) candidates
    memoize getDelimeters

let rec memoizedGetAnswer =
    let rec getAnswer (len, sum) =
        match (len, sum) with
        | (1, sum) -> [(sum, sum)]
        | (len, sum) when len > sum -> []
        | (len, sum) -> 
            let delims =  1 :: memoizedGetDelimiters sum
            let leastPart = delims |> List.map (fun x -> memoizedGetAnswer (len - 1, sum / x)) 
            let zippedNumbersWithResults = List.zip delims leastPart 
            let numbersWithGeneratedPart = List.map (fun (x : int, y: (int * int) list) -> List.map (fun (y1: int * int) -> (x * fst y1, x + snd y1)) y) zippedNumbersWithResults
            let numbersWithGeneratedPartUnfolded = List.concat numbersWithGeneratedPart
            let uniquedNumbers = numbersWithGeneratedPartUnfolded |> Set.ofList |> Set.toList
            //printfn "sum %A len %A uniquedNumbers %A" sum len uniquedNumbers
            uniquedNumbers
        | (_, _) -> []
    memoize getAnswer

let asyncGetAnswer (x, y) =
    async { 
        //printfn "Evaluating for  %A" (x, y)
        return memoizedGetAnswer (x, y) 
    }

let testListForAnswer (l : (int * int) list) : bool =
    match List.tryFind(fun (l1 : int * int) -> fst l1 = snd l1) l with
    | Some(x) -> true
    | None -> false

let findAnswerForLength len seqLen : int =
    let answer = 
        seq {1 .. seqLen}
            |> Seq.map (fun x -> asyncGetAnswer (len, len + x)) 
            |> Async.Parallel 
            |> Async.RunSynchronously 
            |> Seq.tryFind (fun (listAns : (int * int) list) -> testListForAnswer listAns)

    //printfn "listWithAnswers %A" listWithAnswers
    
    match answer with
    | Some(x) ->
        printfn "Answer for len %A is %A" len (fst x.[0])
        fst x.[0]
    | None -> -1

let loadDict (dict : Dictionary<int, int>) =
    let lines = System.IO.File.ReadAllLines(".\\cache.dat");
    for str in lines do
        let idx = str.IndexOf(" ")
        let firstNumber = str.Substring(0, idx)
        let secondNumber = str.Substring(idx + 1)
        let first = int firstNumber
        let second = int secondNumber
        dict.[first] <- second
        printfn "Readed answer for %A = %A" first second
    ()


let saveDict (dict: Dictionary<int, int>) =
    let l = new List<string>()
    for keyValue in dict do
        l.Add(keyValue.Key.ToString() + " " + keyValue.Value.ToString())

    System.IO.File.WriteAllLines(".\\cache.dat", l.ToArray())
    printfn "Written %A values to cache" (l.Count)
    ()


let findAnswerWithAutoGrow len seqLen (dict : Dictionary<int, int>) : int =
    let mutable ans = -1
    let mutable len1 = seqLen

    if not (dict.ContainsKey(len)) then
        while ans = -1 do
            ans <- findAnswerForLength len len1
        
            if ans = -1 then
                printfn "No answer at len %A and seqLen %A" len len1

            len1 <- len1 + 10

        dict.[len] <- ans
        saveDict dict
        ans
    else
        dict.[len]

[<EntryPointAttribute>]
let main args =
    let dummy = memoizedGetAnswer (2, 4)
    let maxLen = int args.[0]
    let maxAddition = int args.[1]

    printfn "Max number: %A. Min len: %A" maxLen maxAddition

    let dict = new Dictionary<int, int>()
    loadDict dict

    let lenghts = [2 .. maxLen]
    let answers = List.map (fun x -> findAnswerWithAutoGrow x maxAddition dict) lenghts
    let uniqueAnswers = answers |> Set.ofList |> Set.toList
    printfn "%A" (List.sum uniqueAnswers)
    0
