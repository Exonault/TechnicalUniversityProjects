package iohomework.homeworkfive;

import java.util.Random;

public class IOHomework5 {
    private final double constant = 5;//c
    private final int maxIterations = 10_000;//k_max
    private final double accuracyF = 0.001;//eps_F
    private final double accuracyX = 0.001;//esp_x
    private final int a = 1;
    private final int b = 7;

    private final Random random = new Random();

    public void calculateMinimum(double initialX1, double initialX2) {
        int iteration = 0;//k
        double roK = constant / (iteration + 1);
        double norm;
        double gammaK;

        while (iteration < maxIterations) {


            iteration++;
        }

    }


    private double getFunctionValue(double x1, double x2, double omega1, double omega2) {
        double result = Math.pow((x1 - omega1), 2) + Math.pow((x2 - omega2), 2);

        return result;

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

    public double nextDoubleBetween2(double min, double max) {
        return (this.random.nextDouble() * (max - min)) + min;
    }

}
