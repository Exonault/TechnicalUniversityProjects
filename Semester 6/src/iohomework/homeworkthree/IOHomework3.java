package iohomework.homeworkthree;

public class IOHomework3 {

    public static void main(String[] args) {
        new IOHomework3().calculateMinimumOfFunction(1.9, 3.2);
    }

    private final double constant = 1;//c
    private final int maxIterations = 50_000;//k_max
    private final double accuracyF = 0.000_1;//eps_F
    private final double accuracyX = 0.000_1;//esp_x
    private final double ax = -5;
    private final double ay = -5;
    private final double bx = 5;
    private final double by = 5;

    public void calculateMinimumOfFunction(double initialX, double initialY) {


        int iteration = 0;//k

        double roK = constant / (iteration + 1);

        double norm = norm(initialX, initialY);

        if (norm == 0) {
            norm = 0.000_01;
        }
        double gammaK = 1 / norm;

        double xK = projection(initialX, ax, bx) * (initialX - roK - gammaK * functionDerivativeX(initialX, initialY));
        double yK = projection(initialY, ay, by) * (initialX - roK - gammaK * functionDerivativeY(initialX, initialY));
        double fK;

        double xKNext = projection(xK, ax, bx) * (xK - roK - gammaK * functionDerivativeX(xK, yK));
        double yKNext = projection(yK, ay, by) * (yK - roK - gammaK * functionDerivativeY(xK, yK));
        double fKNext;

        boolean xStatement;
        boolean yStatement;
        boolean fStatement;

        do {


            if (norm == 0) {
                norm = 0.000_01;
            }

            gammaK = 1 / norm;
            roK = constant / (iteration + 1);

            if ((ax <= xK && xK <= bx)) {
                xK = xKNext;
            } else if (xK < ax) {
                xK = ax;
            } else if (bx < xK) {
                xK = bx;
            }

            if ((ay <= yK && yK <= by)) {
                yK = yKNext;
            } else if (yK < ay) {
                yK = ay;
            } else if (by < yK) {
                yK = by;
            }

            fK = functionValue(xK, yK);


            xKNext = projection(xK, ax, bx) * (xK - roK - gammaK * functionDerivativeX(xK, yK));
            yKNext = projection(yK, ay, by) * (yK - roK - gammaK * functionDerivativeY(xK, yK));

            fKNext = functionValue(xKNext, yKNext);

            xStatement = Math.abs(xK - xKNext) > accuracyX;
            yStatement = Math.abs(yK - yKNext) > accuracyX;
            fStatement = Math.abs(fK - fKNext) > accuracyF;

//            System.out.printf("(xk;yk) = (%.3f;%.3f) \n", xK, yK);
//            System.out.printf("min(F(x,y)) = %.3f \n", fK);
//            System.out.printf("iterations = %d \n", iteration);
//            System.out.println("\n");

            iteration++;

        } while (iteration <= maxIterations && xStatement && yStatement && fStatement);

        System.out.printf("(xk;yk) = (%.3f;%.3f) \n", xK, yK);
        System.out.printf("min(F(x,y)) = %.3f \n", fK);
        System.out.printf("iterations = %d \n", iteration);

    }


    private double projection(double element, double left, double right) {
        if (left <= element && element <= right) {
            return element;
        } else if (element < left) {
            return left;
        } else {
            return right;
        }
    }


    private double norm(double x, double y) {
        double functionDerivativeX = functionDerivativeX(x, y);
        double functionDerivativeY = functionDerivativeY(x, y);

        double result = Math.sqrt(Math.pow(functionDerivativeX, 2) + Math.pow(functionDerivativeY, 2));


        return result;

    }

    private double functionDerivativeX(double x, double y) {
        return (3 * x * x) - (3 * y);
    }

    private double functionDerivativeY(double x, double y) {
        return (3 * y * y) - (3 * x);
    }


    private double functionValue(double x, double y) {
        return Math.pow(x, 3) + Math.pow(y, 3) - 3 * x * y;
    }
}
