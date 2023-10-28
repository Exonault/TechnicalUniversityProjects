package iohomework.homeworkfour;

public class FunctionFour implements Function {

    @Override
    public double getFunctionValue(double x1, double x2, double x3) {
        double result = 2 / x3;

        return result;
    }

    @Override
    public double getNorm(double x1, double x2, double x3) {
        double functionDerivativeX1 = getDerivativeX1(x1, x2, x3);
        double functionDerivativeX2 = getDerivativeX2(x1, x2, x3);
        double functionDerivativeX3 = getDerivativeX3(x1, x2, x3);

        double result = Math.sqrt(Math.pow(functionDerivativeX1, 2)
                + Math.pow(functionDerivativeX2, 2)
                + Math.pow(functionDerivativeX3, 2));

        return result;
    }

    @Override
    public double getDerivativeX1(double x1, double x2, double x3) {
        return 0;
    }

    @Override
    public double getDerivativeX2(double x1, double x2, double x3) {
        return 0;
    }

    @Override
    public double getDerivativeX3(double x1, double x2, double x3) {
        double result = -(2 / (x3 * x3));

        return result;
    }
}
