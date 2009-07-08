open System

let result = 0

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
    if (IsInteger value) then (int)value else (-1:int)
    
// Количество целочисленных путей для конкретного M
let GetAnswerForM (M:int) = 
    1
    
let mutable count = 0
let mutable currentM = 1;

while count < maxCount do
    count <- count + GetAnswerForM currentM
    currentM <- currentM + 1;
       
printfn "Answer: %A" count