<?php

$numbers = [1,2,4,8];
$operators = ["+", "-", "+", "-"];

$steps = ["0"];
$operator = null;
$value = 0;

while (count($numbers) > 0) {
    if (count($operators) > 0 && ($operator === null || rand(0, 10) < 5)) {
        $operatorIndex = null;
        do {
            $operatorIndex = rand(0, count($operators) - 1);
        } while (count($operators) > 1 && $operators[$operatorIndex] == $operator);
        $operator = array_splice($operators, $operatorIndex, 1)[0];
        $steps[] = $operator;
    }

    $numberIndex = rand(0, count($numbers) - 1);
    $number = array_splice($numbers, $numberIndex, 1)[0];
    $steps[] = $number;

    switch ($operator) {
        case '+':
            $value += $number;
            break;
        case '-':
            $value -= $number;
            break;
        case '*':
            $value *= $number;
            break;
    }
}

echo implode(',', $steps) . ' = ' . $value . PHP_EOL;

