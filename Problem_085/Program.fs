#nowarn "40"
open System.Collections.Generic

let memoize f =
    let cache = Dictionary()
    fun x ->
        try
            if cache.ContainsKey(x) then
                cache.[x]
            else
                let res = f x
                cache.[x] <- res

                res
        with 
            | ex ->
                let res = f x
                res 

let max a b =
    match (a,b) with
    | (a,b) when a > b -> a
    | (_,b) -> b

let memoizedCalcuteNewRectanglesCount =
    let calcuteNewRectanglesCount (w, h) =
        let normW = max w h
        let normH = w + h - (max w h)
        let newRectanglesCount = [1 .. normH] |> List.sum |> (*) (normW + 1)
        newRectanglesCount 
    memoize calcuteNewRectanglesCount

let rec memoizedCalculateRectanges =
    let rec calculateRectanges (w, h) =
        match (w, h) with
        | (1, 1) -> 1
        | (w, h) when w < h ->
            memoizedCalculateRectanges (h, w)
        | (w, h) ->
            let newRectangles = memoizedCalcuteNewRectanglesCount ((w - 1), h)
            let previousRectangles = memoizedCalculateRectanges ((w - 1), h)
            newRectangles + previousRectangles
    memoize calculateRectanges

let findAnswer limit =
    let numbers = [for x in 1..limit do
                        for y in 1..limit do
                            yield (x, y)]
    let answers = numbers |> List.map memoizedCalculateRectanges |> List.zip numbers |> List.filter (fun (x, y) -> y <= 2000000) |> List.sortBy (fun (x, y) -> y)
    answers.[answers.Length - 1]
    
printfn "%A" (findAnswer 100)
