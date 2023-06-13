package javabasics.minesweeper;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;

public class StartUp {
    public static void main(String[] args) {
        generateStartFrame();
    }

    public static void generateStartFrame() {
        JFrame frame = new JFrame("Main menu");

        JButton button = new JButton("Submit");
        button.setBounds(100, 100, 140, 40);

        JLabel difficulty = new JLabel();
        difficulty.setText("Enter difficulty :");
        difficulty.setBounds(10, 10, 100, 100);

        JLabel easy = new JLabel();
        easy.setBounds(10, 110, 200, 100);
        easy.setText("Easy - 9x9 with 10 bombs \n");

        JLabel medium = new JLabel();
        medium.setBounds(10, 130, 200, 100);
        medium.setText("Medium - 16x16 with 40 bombs \n");

        JLabel hard = new JLabel();
        hard.setBounds(10, 150, 200, 100);
        hard.setText("Hard - 16x30 with 99 bombs \n");

        JLabel custom = new JLabel();
        custom.setBounds(10, 170, 200, 100);
        custom.setText("Custom - You choose the numbers");

        JTextField textField = new JTextField();
        textField.setBounds(110, 50, 130, 30);
        textField.addKeyListener(new KeyAdapter() {
            @Override
            public void keyPressed(KeyEvent e) {
                if (e.getKeyCode() == KeyEvent.VK_ENTER) {
                    String level = textField.getText();
                    if (level.equalsIgnoreCase("easy")) {
                        startGame(9, 9, 10);
                    } else if (level.equalsIgnoreCase("medium")) {
                        startGame(16, 16, 40);
                    } else if (level.equalsIgnoreCase("hard")) {
                        startGame(16, 30, 99);
                    } else if (level.equalsIgnoreCase("custom")) {
                        customGame();
                    } else startGame(9, 9, 10);
                }
            }
        });

        frame.add(textField);
        frame.add(difficulty);
        frame.add(easy);
        frame.add(medium);
        frame.add(hard);
        frame.add(custom);

        frame.add(button);
        frame.setBounds(500, 200, 300, 300);
        frame.setLayout(null);
        frame.setVisible(true);
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setResizable(false);

        button.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent arg0) {
                String level = textField.getText();
                if (level.equalsIgnoreCase("easy")) {
                    startGame(9, 9, 10);
                } else if (level.equalsIgnoreCase("medium")) {
                    startGame(16, 16, 40);
                } else if (level.equalsIgnoreCase("hard")) {
                    startGame(16, 30, 99);
                } else if (level.equalsIgnoreCase("custom")) {
                    customGame();
                } else StartUp.startGame(9, 9, 10);
            }
        });
    }

    public static void startGame(int rows, int cols, int bombs) {//Generating the game
        JFrame frame = new JFrame("MineSweeper");
        frame.setLayout(new BorderLayout());
        frame.setResizable(false);
        frame.add(new Minesweeper(rows, cols, bombs));
        frame.setLocationRelativeTo(null);
        frame.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
        frame.setVisible(true);
        frame.setSize(700, 600);
        frame.setLocation(400, 150);
    }

    public static void customGame() {
        JFrame frame = new JFrame("Custom mode menu");
        JTextField columns, rows, bombs;

        columns = new JTextField("Columns", 10);
        rows = new JTextField("Rows", 10);
        bombs = new JTextField("Bombs", 10);

        JButton submitButton = new JButton("Submit");
        submitButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                int numberRows = Integer.parseInt(rows.getText());
                int numberCols = Integer.parseInt(columns.getText());
                int numberBombs = Integer.parseInt(bombs.getText());
                startGame(numberRows, numberCols, numberBombs);
            }
        });

        frame.getContentPane().setLayout(new FlowLayout());
        frame.getContentPane().add(rows);
        frame.getContentPane().add(columns);
        frame.getContentPane().add(bombs);
        frame.getContentPane().add(submitButton);

        frame.pack();
        frame.setVisible(true);
        frame.setResizable(false);
        frame.setLocation(500, 200);
        frame.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
    }
}