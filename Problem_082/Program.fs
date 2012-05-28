// Learn more about F# at http://fsharp.net
let readMatrix = 
    let fileContentAsList = System.IO.File.ReadAllLines(@"matrix.txt")
    fileContentAsList 
        |> Array.map (fun x -> x.Split(',')) 
        |> Array.map (fun x -> Array.map (fun y -> System.Int32.Parse(y)) x)

let min a b =
    match (a, b) with
    | (a, b) when a > b -> b
    | _ -> a

let solve =
    let matrix = readMatrix
    let answers = Array.init 80 (fun y -> 100000000)
    let marks = Array.map (fun x -> Array.init 80 (fun y -> 0)) (Array.init 80 (fun y -> 0))

    let rec calculateForCell x y partialAnswer =
        match (x, y) with
        | (x, 79) -> 
            if ((partialAnswer + matrix.[x].[y]) < answers.[x]) then
                answers.[x] <- (partialAnswer + matrix.[x].[y]) 
                printf "*"
                //printfn "[%A %A] = %A" x y (Array.sum answers)

        | (x, y) when marks.[x].[y] = 0 -> 
            marks.[x].[y] <- 1
            
            if x < 79 && marks.[x + 1].[y] = 0 then
                calculateForCell (x + 1) y (partialAnswer + matrix.[x].[y])
            
            if x > 0 && marks.[x - 1].[y] = 0 then
                calculateForCell (x - 1) y (partialAnswer + matrix.[x].[y])

            if y < 79 && marks.[x].[y + 1] = 0 then
                calculateForCell x (y + 1) (partialAnswer + matrix.[x].[y])

            marks.[x].[y] <- 0
        | (x, y) ->  
            printfn "%A %A" x y
            ()

    calculateForCell 0 0 0
    answers

let ans = solve
printfn "%A %A" ans (Array.min ans)
