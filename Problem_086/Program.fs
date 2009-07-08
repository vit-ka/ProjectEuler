open System

// Максимальное возможное количество решений.
let maxCount = 1000000

// Является ли число целым.
let IsInteger (a:float) =
    a = (float)((int)a)

// Кратчайший путь из вершины параллелепипеда в противолежащую.
let GetShortestPath (a:int) (b:int) (c:int) = 
    let sum = b + c
    let sq = a * a + sum * sum
    let value = Math.Sqrt((float)sq)
    value
    
// Минимальное значение в листе.
let rec Min x =
    match x with
        | [] -> invalid_arg "x is empty"
        | head::[] -> head
        | head::tail -> if head < (Min tail) then head else (Min tail)
    
// Количество целочисленных путей для конкретного M
let GetAnswerForM (M:int) = 
    //printfn "Finding paths at size %A" M
    let mutable result:int = 0
    for a in 1..M do
        for b in a..M do
            let mutable results = []
            
            results <- ((GetShortestPath a b M) :: results)
                
            if a <> b then
                results <- ((GetShortestPath b a M) :: results)
                
            if a <> M then
                results <- ((GetShortestPath M b a) :: results)
                
            let minValue = Min results
            
            if IsInteger minValue then
                //printfn "Path %A %A %A is %A" a b M minValue
                result <- result + 1
                            
    result
    
let mutable count = 0
let mutable currentM = 1;

while count <= maxCount do
    count <- count + GetAnswerForM currentM
    printfn "Current count with M = %A : %A" currentM count
    currentM <- currentM + 1;
           
printfn "For M = %A answer: %A" (currentM - 1) count