package iohomework.homeworktwo;

import java.math.BigDecimal;

public class IOHomework2 {
    
    public void calculateMinimumOfFunction(double initialX, double initialY) {
        int iteration = 1;//k
        final double constant = 10;//c
        int maxIterations = 50_000;//k_max
        double accuracyF = 0.000_1;//eps_F
        double accuracyX = 0.000_1;//esp_x

        // double roK = constant / (iteration + 1);
        double roK = constant / (iteration + 1);

        double norm = norm(initialX, initialY);

        double gammaK = 1 / (norm);

        double xK = initialX - (roK * gammaK * functionDerivativeX(initialX, initialY));
        double yK = initialY - (roK * gammaK * functionDerivativeY(initialX, initialY));

        double xKNext = xK - (roK * gammaK * functionDerivativeX(xK, yK));
        double yKNext = yK - (roK * gammaK * functionDerivativeY(xK, yK));

        double Fk = functionValue(xK, yK);
        double FkNext = functionValue(xKNext, yKNext);

        boolean xStatement = Math.abs(xK - xKNext) > accuracyX;
        boolean yStatement = Math.abs(yK - yKNext) > accuracyX;
        boolean fStatement = Math.abs(Fk - FkNext) > accuracyF;


        while (iteration <= maxIterations && xStatement && yStatement && fStatement) {
            iteration++;

            xK = xKNext;
            yK = yKNext;
            Fk = functionValue(xK, yK);
            norm = norm(xK, yK);
            roK = constant / (iteration + 1);

            if (norm != 0) {
                gammaK = 1 / norm;
            } else {
                norm = Math.pow(10, -25);
                gammaK = 1 / norm;
            }

            xKNext = xK - (roK * gammaK * functionDerivativeX(xK, yK));
            yKNext = yK - (roK * gammaK * functionDerivativeY(xK, yK));

            FkNext = functionValue(xKNext, yKNext);


            xStatement = Math.abs(xK - xKNext) > accuracyX;
            yStatement = Math.abs(yK - yKNext) > accuracyX;
            fStatement = Math.abs(Fk - FkNext) > accuracyF;


        }

        System.out.printf("(xk;yk) = (%.3f;%.3f) \n", xK, yK);
        System.out.printf("min(F(x,y)) = %.3f \n", Fk);
        System.out.printf("iterations = %d \n", iteration);

    }


    private double norm(double x, double y) {
        double functionDerivativeX = functionDerivativeX(x, y);
        double functionDerivativeY = functionDerivativeY(x, y);

        double result = Math.sqrt(Math.pow(functionDerivativeX, 2) + Math.pow(functionDerivativeY, 2));


        return result;

    }

    private double functionDerivativeX(double x, double y) {
        return 2 * x + y;
    }

    private double functionDerivativeY(double x, double y) {
        return x + 2 * y;
    }


    private double functionValue(double x, double y) {
        return (x * x) + (x * y) + (y * y) - 10;
    }
}
