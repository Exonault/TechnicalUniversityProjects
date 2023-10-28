package iohomework.homeworkfive;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

public class IOHomework5 {
    private final double constant = 5;//c
    private final int maxIterations = 10_000;//k_max
    private final int a = 1;
    private final int b = 7;

    private final Random random = new Random();

    public void calculateMinimum(double initialX1, double initialX2) {
        int iteration = 0;//k
        double roK;
        double norm;
        double gammaK;

        double omega1 = getNextRandomBetweenTwoValues();
        double omega2 = getNextRandomBetweenTwoValues();

        List<Double> functionValuesX1 = new ArrayList<>();
        List<Double> functionValuesX2 = new ArrayList<>();

        List<Double> functionNormValues = new ArrayList<>();


        double xk1 = initialX1;
        double xk2 = initialX2;

        while (iteration < maxIterations) {
            functionValuesX1.add(xk1);
            functionValuesX2.add(xk2);

            double expectedValueX1 = getExpectedValue(functionValuesX1, iteration);
            double expectedValueX2 = getExpectedValue(functionValuesX2, iteration);

            norm = norm(xk1, xk2, omega1, omega2);
            functionNormValues.add(norm);

            double expectedNormValue = getExpectedValue(functionNormValues, iteration);
            gammaK = 1 / expectedNormValue;
            roK = constant / (iteration + 1);

            xk1 = xk1 - (roK * gammaK * expectedValueX1);
            xk2 = xk2 - (roK * gammaK * expectedValueX2);

            iteration++;
        }

        System.out.printf("(x1k;x2k) = (%.5f;%.5f) \n", xk1, xk2);
        System.out.printf("min(F(x,y)) = %.3f \n", getFunctionValue(xk1, xk2, omega1, omega2));
        System.out.printf("iterations = %d \n", iteration);

    }


    private double getFunctionValue(double x1, double x2, double omega1, double omega2) {
        double result = Math.pow((x1 - omega1), 2) + Math.pow((x2 - omega2), 2);

        return result;

    }

    private double getExpectedValue(List<Double> list, int iteration) {
        double sum = 0;
        for (Double element : list) {
            sum += element;
        }
        return sum / (iteration + 1);
    }


    private double norm(double x1, double x2, double omega1, double omega2) {
        double functionDerivativeX1 = getFunctionDerivativeX1(x1, x2, omega1, omega2);
        double functionDerivativeX2 = getFunctionDerivativeX2(x1, x2, omega1, omega2);

        double result = Math.sqrt(Math.pow(functionDerivativeX1, 2) + Math.pow(functionDerivativeX2, 2));


        return result;

    }


    private double getFunctionDerivativeX1(double x1, double x2, double omega1, double omega2) {
        double result = 2 * (x1 - omega1);

        return result;
    }

    private double getFunctionDerivativeX2(double x1, double x2, double omega1, double omega2) {
        double result = 2 * (x2 - omega2);

        return result;
    }

    private double getNextRandomBetweenTwoValues() {
        return (this.random.nextDouble() * (b - a)) + a;
    }

}
