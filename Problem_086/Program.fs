open System

let result = 0

// Максимальное возможная длина пути.
let maxNumber = 1000000

// Является ли число целым.
let IsInteger (a:float) =
    a = (float)((int)a)

// Кратчайший путь из вершины параллелепипеда в противолежащую.
let GetShortestPath (a:int) (b:int) (c:int) = 
    let sum = b + c
    let sq = a * a + sum * sum
    let value = Math.Sqrt((float)sq)
    if (IsInteger value) then (int)value else (-1:int)
       
printfn "Answer: %A" (GetShortestPath 6 5 3)