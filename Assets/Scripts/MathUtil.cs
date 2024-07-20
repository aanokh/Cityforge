using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Alexander Anokhin

public static class MathUtil {
    
    public static bool isEven(int n) {
        if (n % 2 == 0) {
            return true;
        } else {
            return false;
        }
    }

    public static bool isOdd(int n) {
        return !isEven(n);
    }

    public static int roundUpToEven(int n) {
        if (isOdd(n)) {
            n++;
        }

        return n;
    }
}
