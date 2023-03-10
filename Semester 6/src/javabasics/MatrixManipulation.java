package javabasics;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Arrays;
import java.util.Scanner;

public class MatrixManipulation {
    private final static Scanner input = new Scanner(System.in);

    public void executeProgram() {


        int[][] matrix = createMatrix();
        int size = matrix.length; // square matrix
        printMatrix(matrix, size);

        int sumAllElements = sumAllElements(matrix, size);
        System.out.printf("Sum of all elements: %d \n", sumAllElements);

        int[] sumOfAllRows = sumOfAllRows(matrix, size);
        printArray(sumOfAllRows);

        int[] sumOfAllColumns = sumOfAllColumns(matrix, size);
        printArray(sumOfAllColumns);

        int sumOfAllColumnsMin = getMin(sumOfAllColumns);
        int sumOfAllColumnsMax = getMax(sumOfAllColumns);
        int sumOfAllRowsMin = getMin(sumOfAllRows);
        int sumOfAllRowsMax = getMax(sumOfAllRows);

        System.out.printf("Min of all columns: %d \n", sumOfAllColumnsMin);
        System.out.printf("Max of all columns: %d \n", sumOfAllColumnsMax);
        System.out.printf("Min of all rows: %d \n", sumOfAllRowsMin);
        System.out.printf("Max of all rows: %d \n", sumOfAllRowsMax);

        writeToFile(matrix, size);

    }


    private int[][] createMatrix() {

        System.out.println("Enter the size of the matrix");
        int size = input.nextInt();

        int[][] matrix = new int[size][size];

        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                System.out.printf("Enter a number for [%d,%d] \n", i, j);
                int value = input.nextInt();
                matrix[i][j] = value;
            }
        }

        return matrix;

    }


    private int sumAllElements(int[][] matrix, int size) {
        int result = 0;
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                int value = matrix[i][j];
                result += value;
            }

        }

        return result;

    }

    private int[] sumOfAllRows(int[][] matrix, int size) {
        int[] result = new int[size];

        for (int i = 0; i < size; i++) {
            int value = 0;
            for (int j = 0; j < size; j++) {
                value += matrix[i][j];
            }
            result[i] = value;
        }
        return result;
    }

    private int[] sumOfAllColumns(int[][] matrix, int size) {
        int[] result = new int[size];

        for (int i = 0; i < size; i++) {
            int value = 0;
            for (int j = 0; j < size; j++) {
                value += matrix[j][i];
            }
            result[i] = value;
        }
        return result;
    }

    private int getMin(int[] array) {
        if (array.length == 0) {
            return 0;
        } else return Arrays.stream(array).min().getAsInt();
    }

    private int getMax(int[] array) {
        if (array.length == 0) {
            return 0;
        } else return Arrays.stream(array).max().getAsInt();
    }

    private void writeToFile(int[][] matrix, int size) {
        try {
            File file = new File("filename.txt");
            if (!file.exists()) {
                file.createNewFile();
                //System.out.println("File created: " + myObj.getName());

            }

            FileWriter fw = new FileWriter(file);
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    int value = matrix[i][j];
                    fw.write(value + "\t");

                }
                fw.write("\n");
            }
            fw.close();

        } catch (IOException e) {
            System.out.println("An error occurred.");
            e.printStackTrace();
        }
    }

    private void printMatrix(int[][] matrix, int size) {
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                int value = matrix[i][j];
                System.out.printf("%d \t", value);
            }
            System.out.println();
        }
    }

    private void printArray(int[] array) {
        for (int i : array) {
            System.out.printf("%d \t", i);
        }
        System.out.println();
    }


}
