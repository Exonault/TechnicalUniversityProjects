package iohomework.homeworksix;

public class IOHomework6 {
    public void calculateMinimum() {
        int[] x = {7, 5, 3, 5, 5};
        int[] D = {6, 6, 5, 4, 4};
        int[] c = {5500, 3550, 2320, 3960, 4550};
        int[] b = {0, 4550, 3270, 0, 0};
        int[] h = {4715, 0, 0, 3168, 3640};
        double[] p = {0.26, 0.35, 0.09, 0.13, 0.17};

        double ED = 0;

        for (int i = 0; i < 4; i++) {
            ED+= p[i]*D[i];
        }

        double EF = 0;
        for (int i = 0; i < 4; i++) {
            double arg1 = (c[i]-b[i])*x[i]*ED;
            double arg2 = (c[i]+h[i])*x[i] - h[i]*ED;
            EF = Math.max(arg1,arg2);
        }

        System.out.printf("E[f(x,D)] = %.3f \n", EF);
    }


}
