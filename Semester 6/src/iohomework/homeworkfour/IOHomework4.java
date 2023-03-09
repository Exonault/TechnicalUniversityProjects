package iohomework.homeworkfour;


import java.util.Arrays;
import java.util.List;

public class IOHomework4 {

    private final double constant = 5;//c
    private final int maxIterations = 20_000; //50_000;//k_max
    private final double accuracyF = 0.001;//eps_F
    private final double accuracyX = 0.001;//esp_x


    public void calculateMinimum(final double initialX1, final double initialX2, final double initialX3) {//x1_0, x2_0, x3_0

        List<Function> functions = Arrays.asList(new FunctionOne(), new FunctionTwo(), new FunctionThree(), new FunctionFour());

        Function maxFromAllFunctions = getMaxFromAllFunctions(functions, initialX1, initialX2, initialX3);

        int iteration = 0;//k
        double roK = constant / (iteration + 1);

        double norm = maxFromAllFunctions.getNorm(initialX1, initialX2, initialX3);

        if (norm == 0) {
            norm = Double.MIN_VALUE;
        }

        double gammaK = 1 / norm;

        double x1K = initialX1 - (roK * gammaK * maxFromAllFunctions.getDerivativeX1(initialX1, initialX2, initialX3));
        double x2K = initialX2 - (roK * gammaK * maxFromAllFunctions.getDerivativeX2(initialX1, initialX2, initialX3));
        double x3K = initialX3 - (roK * gammaK * maxFromAllFunctions.getDerivativeX3(initialX1, initialX2, initialX3));
        double fK = maxFromAllFunctions.getFunctionValue(x1K, x2K, x3K);

        double x1KNext;
        double x2KNext;
        double x3KNext;
        double fKNext;

        boolean x1Statement;
        boolean x2Statement;
        boolean x3Statement;
        boolean fStatement;

        do {

            iteration++;
            roK = constant / (iteration + 1);
            norm = maxFromAllFunctions.getNorm(x1K, x2K, x3K);

            if (norm == 0) {
                norm = Double.MIN_VALUE;
            }

            gammaK = 1 / norm;


            x1KNext = x1K - (roK * gammaK * maxFromAllFunctions.getDerivativeX1(x1K, x2K, x3K));
            x2KNext = x2K - (roK * gammaK * maxFromAllFunctions.getDerivativeX2(x1K, x2K, x3K));
            x3KNext = x3K - (roK * gammaK * maxFromAllFunctions.getDerivativeX3(x1K, x2K, x3K));

            fKNext = maxFromAllFunctions.getFunctionValue(x1KNext, x2KNext, x3KNext);

            x1Statement = Math.abs(x1K - x1KNext) > accuracyX;
            x2Statement = Math.abs(x2K - x2KNext) > accuracyX;
            x3Statement = Math.abs(x3K - x3KNext) > accuracyX;
            fStatement = Math.abs(fK - fKNext) > accuracyF;

            x1K = x1KNext;
            x2K = x2KNext;
            x3K = x3KNext;
            fK = fKNext;

        }

        while (iteration <= maxIterations && x1Statement && x2Statement && x3Statement && fStatement);

        System.out.printf("(x1k;x2k;x3k) = (%.3f;%.3f;%.3f) \n", x1K, x2K, x3K);
        System.out.printf("min(F(x1,x2,x3)) = %.3f \n", fK);
        System.out.printf("iterations = %d \n", iteration);

    }


    private Function getMaxFromAllFunctions(List<Function> functions, double initialX1, double initialX2, double initialX3) {

        double currentMax = Double.MIN_VALUE;
        for (Function function : functions) {
            double functionValue = function.getFunctionValue(initialX1, initialX2, initialX3);

            if (functionValue > currentMax) {
                currentMax = functionValue;
            }

        }

        for (Function function : functions) {
            double functionValue = function.getFunctionValue(initialX1, initialX2, initialX3);

            if (functionValue == currentMax) {
                return function;
            }

        }

        return functions.get(0);
    }


}

