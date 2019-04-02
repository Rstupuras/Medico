package view;

import controller.MedicoController;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.HBox;
import javafx.stage.Stage;


public class LoginMenu {
    private String password;
    private String username;
    private Stage loginMenu;
    private TextField usernameField;
    private PasswordField passwordField;
    private MedicoController controller;



    public LoginMenu(MedicoController controller) {
        this.loginMenu = new Stage();
        this.loginMenu = loginMenu();
        this.controller = controller;

    }


    public Stage loginMenu() {
        Image image = new Image("images/logo.png");
        ImageView imageView = new ImageView();
        imageView.setImage(image);
        HBox hBox = new HBox();
        hBox.setSpacing(50);
        hBox.setAlignment(Pos.CENTER);
        GridPane gridPane = new GridPane();
        gridPane.setAlignment(Pos.CENTER_LEFT);
        Label label = new Label("User name");
        this.usernameField = new TextField();
        Label label1 = new Label("Password");
        this.passwordField = new PasswordField();
        Button loginButton = new Button("Log in");
        Button loginAdminButton = new Button("Log in as admin");

        loginButton.setOnAction(event -> {
                if (usernameField.getText().length() == 0 || passwordField.getText().length() == 0 )
                {
                    wrongLogin();
                }
                else {
                    setPassword(passwordField.getText());
                    setUserName(usernameField.getText());
                    controller.execute("login");
                }


        });

        loginAdminButton.setOnAction(event -> {
            if (usernameField.getText().length() == 0 || passwordField.getText().length() == 0 )
            {
                wrongLogin();
            }
            else {
                setPassword(passwordField.getText());
                setUserName(usernameField.getText());
                controller.execute("loginAsAdmin");
            }

        });

        gridPane.setHgap(5);
        gridPane.setVgap(5);
        GridPane.setConstraints(label, 0, 0);
        GridPane.setConstraints(usernameField,0,1);
        GridPane.setConstraints(label1, 0, 2);
        GridPane.setConstraints(passwordField, 0, 3);
        GridPane.setConstraints(loginButton, 0, 5);
        GridPane.setConstraints(loginAdminButton,0,7);
        gridPane.getChildren().addAll(label, usernameField, label1, passwordField, loginButton, loginAdminButton);
        hBox.getChildren().addAll(gridPane,imageView);
        Scene scene = new Scene(hBox, 700, 400);
        loginMenu.setResizable(false);
        loginMenu.setTitle("Medico");
        loginMenu.setScene(scene);
        return loginMenu;

    }

    public void showStage() {
        loginMenu.show();
    }

    public void closeStage() {

        passwordField.setText("");
        usernameField.setText("");
        usernameField.requestFocus();
        loginMenu.close();
    }


    public String getUsername() {
        return username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public void setUserName(String username) {
        this.username = username;
    }

    public void wrongLogin()
    {
        Alert alert = new Alert(Alert.AlertType.ERROR);
        alert.setHeaderText(null);
        alert.setContentText("Username or password is not correct");
        alert.showAndWait();
    }


}
