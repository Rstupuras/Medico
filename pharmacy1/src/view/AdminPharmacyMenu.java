package view;
import controller.MedicoController;
import domain.model.Medicament;
import domain.model.Pharmacy;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.collections.transformation.FilteredList;
import javafx.event.EventHandler;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.HBox;
import javafx.scene.layout.Pane;
import javafx.scene.layout.VBox;
import javafx.scene.text.Text;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.collections.transformation.FilteredList;
import javafx.event.EventHandler;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.HBox;
import javafx.scene.layout.Pane;
import javafx.scene.layout.VBox;
import javafx.scene.text.Text;
import javafx.stage.Stage;

import java.util.List;
import java.util.Timer;
import java.util.TimerTask;


public class AdminPharmacyMenu {
    private Stage adminMenu;
    private FilteredList<Pharmacy> filteredPharmacyData;
    private TableView tableViewPharmacy;
    private ObservableList<Pharmacy> data;
    private Stage edit;
    private Stage add;
    private TextField name;
    private TextField id;
    private TextField email;
    private TextField phoneNumber;
    private TextField location;
    private TextField username;
    CheckBox cb1;
    private PasswordField password;
    private Text successfulOperation;
    private MedicoController controller;

    public AdminPharmacyMenu(MedicoController controller) {
        this.tableViewPharmacy = new TableView();
        this.adminMenu = new Stage();
        this.adminMenu = adminMenu();
        this.controller = controller;
    }

    public void setPharmacyItems(List<Pharmacy> pharmacyList) {
        data = FXCollections.observableArrayList();
        for (int i = 0; i < pharmacyList.size(); i++) {
            data.add(pharmacyList.get(i));
        }
        this.filteredPharmacyData = new FilteredList<>(data, p -> true);
        tableViewPharmacy.setItems(data);
        tableViewPharmacy.setItems(filteredPharmacyData);
    }

    private Stage adminMenu() {
        tableViewPharmacy.setEditable(true);
        adminMenu.setResizable(false);
        VBox vBox = new VBox();
        HBox forImage = new HBox();
        forImage.setPadding(new Insets(0, 560, 0, 0));
        forImage.setAlignment(Pos.TOP_LEFT);
        HBox hBox = new HBox();
        hBox.setAlignment(Pos.TOP_RIGHT);
        Button addPharmacyButton = new Button("Add");
        addPharmacyButton.setPrefSize(100, 40);
        hBox.setPadding(new Insets(20, 20, 20, 20));
        hBox.setSpacing(40);
        Button deletePharmacyButton = new Button("Delete");
        deletePharmacyButton.setPrefSize(100, 40);
        Button refreshButton = new Button("Refresh");
        refreshButton.setPrefSize(80,35);
        hBox.getChildren().addAll(forImage, addPharmacyButton, deletePharmacyButton);
        HBox hBox1 = new HBox();
        hBox1.setAlignment(Pos.CENTER);
        HBox hBox2 = new HBox();
        hBox2.setAlignment(Pos.BOTTOM_RIGHT);
        hBox2.setPadding(new Insets(20, 20, 20, 20));

        deletePharmacyButton.setOnAction(event -> {
            Pharmacy tmp = (Pharmacy) tableViewPharmacy.getSelectionModel().getSelectedItem();
            if(tableViewPharmacy.getItems().isEmpty())
            {

            }
            else if(tableViewPharmacy.getSelectionModel().isEmpty())
            {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setTitle("Pharmacy has not been selected");
                alert.setHeaderText(null);
                alert.setContentText("Please select pharmacy to delete");
                alert.showAndWait();
            }
            else if(!(tmp.hasOrders()))
            {
                controller.execute("deletePharmacy");
                completeAction("Pharmacy deleted", successfulOperation);
            }
            else
            {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setTitle("Pharmacy cannot be deleted");
                alert.setHeaderText(null);
                alert.setContentText("Pharmacy cannot be deleted because it has orders");
                alert.showAndWait();
            }
        });


        addPharmacyButton.setOnAction(event ->
        {
            addPharmacy();
        });

        refreshButton.setOnAction(event -> {
            controller.execute("RefreshPharmacies");
        });


        TextField textField = new TextField();
        textField.setPromptText("Search by name");
        successfulOperation = new Text();
        successfulOperation.setStyle("-fx-font: 20 arial;");
        HBox hbox3 = new HBox();
        hbox3.setPadding(new Insets(0,0,0,100));
        hbox3.getChildren().addAll(refreshButton);
        hbox3.setSpacing(5);
        hBox2.getChildren().addAll(successfulOperation,hbox3, textField);
        hBox2.setSpacing(10);

        textField.setOnKeyReleased(event -> {
            filteredPharmacyData.setPredicate(pharmacy -> pharmacy.getName().toLowerCase().contains(textField.getText().toLowerCase().trim()));
        });


        TableColumn nameCol = new TableColumn("Name");
        TableColumn idCol = new TableColumn("ID");
        TableColumn usernameCol = new TableColumn("User name");
        TableColumn emailCol = new TableColumn("Email");
        TableColumn phoneNumberCol = new TableColumn("Phone number");
        TableColumn locationCol = new TableColumn("Location");
        TableColumn isAdminCol = new TableColumn("Admin");


        nameCol.setCellValueFactory(new PropertyValueFactory<Pharmacy, String>("name"));
        idCol.setCellValueFactory(new PropertyValueFactory<Pharmacy, String>("id"));
        usernameCol.setCellValueFactory(new PropertyValueFactory<Pharmacy, String>("username"));
        emailCol.setCellValueFactory(new PropertyValueFactory<Pharmacy, String>("email"));
        phoneNumberCol.setCellValueFactory(new PropertyValueFactory<Pharmacy, String>("phoneNumber"));
        locationCol.setCellValueFactory(new PropertyValueFactory<Pharmacy, String>("location"));
        isAdminCol.setCellValueFactory(new PropertyValueFactory<Pharmacy, Boolean>("isAdmin"));

        nameCol.setMinWidth(150);
        idCol.setMinWidth(50);
        usernameCol.setMinWidth(150);
        emailCol.setMinWidth(150);
        phoneNumberCol.setMinWidth(150);
        locationCol.setMinWidth(220);
        isAdminCol.setMinWidth(30);



        tableViewPharmacy.getColumns().addAll(nameCol, idCol, usernameCol, emailCol, phoneNumberCol, locationCol, isAdminCol);
        tableViewPharmacy.setMinHeight(480);
        tableViewPharmacy.setMinWidth(1010);
        hBox1.getChildren().add(tableViewPharmacy);

        vBox.getChildren().addAll(hBox, hBox1, hBox2);

        tableViewPharmacy.setOnMouseClicked(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                if (event.getClickCount() == 2) {
                    if (tableViewPharmacy.getSelectionModel().getSelectedItem() != null) {
                        editPharmacy(tableViewPharmacy.getSelectionModel());
                    }
                }
            }
        });


        Scene scene = new Scene(vBox, 1000, 650);
        adminMenu.setScene(scene);
        adminMenu.setTitle("Pharmacy list");
        return adminMenu;
    }

    public void editPharmacy(SelectionModel selectionModel) {
        Pharmacy pharmacy = (Pharmacy) selectionModel.getSelectedItem();
        edit = new Stage();
        GridPane root = new GridPane();
        root.setPadding(new Insets(20, 20, 20, 20));
        root.setVgap(10);
        root.setHgap(10);
        root.setAlignment(Pos.TOP_CENTER);
        name = new TextField();
        username = new TextField();
        email = new TextField();
        phoneNumber = new TextField();
        location = new TextField();
        cb1 = new CheckBox();
        cb1.setText("Admin");
        Button save = new Button("Save");

        save.setPrefSize(70, 30);
        Button cancel = new Button("Cancel");
        cancel.setPrefSize(70, 30);
        Pane pane1 = new Pane();
        pane1.minHeightProperty().bind(name.heightProperty());
        Pane pane2 = new Pane();
        pane2.minHeightProperty().bind(name.heightProperty());
        Pane pane3 = new Pane();
        pane3.minHeightProperty().bind(name.heightProperty());
        Pane pane4 = new Pane();
        pane4.minHeightProperty().bind(name.heightProperty());
        Pane pane5 = new Pane();
        pane5.minHeightProperty().bind(name.heightProperty());
        Pane pane6 = new Pane();
        pane6.minHeightProperty().bind(name.heightProperty());
        Label label = new Label("Name:");
        Label label2 = new Label("User name:");
        Label label3 = new Label("Email:");
        Label label4 = new Label("Phone number:");
        Label label5 = new Label("Location:");


        GridPane.setConstraints(name, 3, 1);
        GridPane.setColumnSpan(name, 3);
        GridPane.setConstraints(username, 3, 2);
        GridPane.setColumnSpan(username, 3);
        GridPane.setConstraints(email, 3, 3);
        GridPane.setColumnSpan(email, 3);
        GridPane.setConstraints(phoneNumber, 3, 4);
        GridPane.setColumnSpan(phoneNumber, 3);
        GridPane.setConstraints(location, 3, 5);
        GridPane.setColumnSpan(location, 3);
        GridPane.setConstraints(label, 0, 1);
        GridPane.setColumnSpan(label, 3);
        GridPane.setConstraints(label2, 0, 2);
        GridPane.setColumnSpan(label2, 3);
        GridPane.setConstraints(label3, 0, 3);
        GridPane.setColumnSpan(label3, 3);
        GridPane.setConstraints(label4, 0, 4);
        GridPane.setColumnSpan(label4, 3);
        GridPane.setConstraints(label5, 0, 5);
        GridPane.setColumnSpan(label5, 3);
        GridPane.setConstraints(cb1,0,6);
        GridPane.setColumnSpan(cb1,3);
        GridPane.setConstraints(pane6, 0, 3);
        GridPane.setConstraints(pane5, 0, 4);
        GridPane.setConstraints(pane4, 0, 5);
        GridPane.setConstraints(pane3, 0, 6);
        GridPane.setConstraints(pane1, 0, 7);
        GridPane.setConstraints(pane2, 0, 8);
        GridPane.setConstraints(save, 0, 9);
        GridPane.setColumnSpan(save, 2);
        GridPane.setConstraints(cancel, 5, 9);
        GridPane.setColumnSpan(cancel, 2);

        root.getChildren().addAll(name, username, email, phoneNumber, location, label, label2, label3, label4, label5, cb1, save, cancel, pane1, pane2, pane3, pane4, pane5, pane6);

        name.setText(pharmacy.getName());
        username.setText(pharmacy.getUsername());
        email.setText(pharmacy.getEmail());
        phoneNumber.setText(pharmacy.getPhoneNumber());
        location.setText(pharmacy.getLocation());
        cb1.setSelected(pharmacy.getIsAdmin());
        save.setOnAction(event -> {


                if(name.getText().isEmpty())
                {
                    name.setStyle("-fx-prompt-text-fill: red");
                    name.setPromptText("Name");
                }
                else if (name.getText().length() > 25) {
                    Alert alert = new Alert(Alert.AlertType.ERROR);
                    alert.setHeaderText("Error");
                    alert.setContentText("Name cannot be longer than 25 letters");
                    alert.showAndWait();
                }

                else if(username.getText().length() <5)
                {
                    Alert alert = new Alert(Alert.AlertType.ERROR);
                    alert.setHeaderText("Error");
                    alert.setContentText("Username should be between 5 and 15 characters");
                    alert.showAndWait();
                }
                else if(username.getText().length() > 15)
                {
                    Alert alert = new Alert(Alert.AlertType.ERROR);
                    alert.setHeaderText("Error");
                    alert.setContentText("Username should be between 5 and 15 characters");
                    alert.showAndWait();
                }
                else if(email.getText().length() > 25)
                {
                    Alert alert = new Alert(Alert.AlertType.ERROR);
                    alert.setHeaderText("Error");
                    alert.setContentText("Email should be between 4 and 25 characters");
                    alert.showAndWait();
                }
                else if(email.getText().length() < 4)
                {
                    Alert alert = new Alert(Alert.AlertType.ERROR);
                    alert.setHeaderText("Error");
                    alert.setContentText("Email should be between 4 and 25 characters");
                    alert.showAndWait();
                }
                else if(phoneNumber.getText().length() < 3)
                {
                    Alert alert = new Alert(Alert.AlertType.ERROR);
                    alert.setHeaderText("Error");
                    alert.setContentText("Phone number should be between 3 and 12 characters");
                    alert.showAndWait();
                }
                else if(phoneNumber.getText().length() > 12)
                {
                    Alert alert = new Alert(Alert.AlertType.ERROR);
                    alert.setHeaderText("Error");
                    alert.setContentText("Phone number should be between 3 and 12 characters");
                    alert.showAndWait();
                }
                else if(location.getText().length() < 8)
                {
                    Alert alert = new Alert(Alert.AlertType.ERROR);
                    alert.setHeaderText("Error");
                    alert.setContentText("Location should be no shorter than 8 characters");
                    alert.showAndWait();
                }
                else if (pharmacy.hasOrders())
                {
                    Alert alert = new Alert(Alert.AlertType.ERROR);
                    alert.setHeaderText("Error");
                    alert.setContentText("Pharmacy cannot be edited because it has orders");
                    alert.showAndWait();
                }
                else {
                    controller.execute("editPharmacy");
                    completeAction("Pharmacy edited", successfulOperation);
                    edit.close();
                }



        });


        cancel.setOnAction(event -> edit.close());

        Scene scene = new Scene(root, 400, 400);
        edit.setScene(scene);
        edit.show();
    }

    public void addPharmacy() {
        add = new Stage();
        GridPane root = new GridPane();
        root.setPadding(new Insets(20, 20, 20, 20));
        root.setVgap(10);
        root.setHgap(10);
        root.setAlignment(Pos.TOP_CENTER);
        name = new TextField();
        username = new TextField();
        password = new PasswordField();
        email = new TextField();
        phoneNumber = new TextField();
        location = new TextField();
        cb1 = new CheckBox();
        Button save = new Button("Save");

        cb1.setText("Admin");
        save.setPrefSize(70, 30);
        Button cancel = new Button("Cancel");
        cancel.setPrefSize(70, 30);
        Pane pane1 = new Pane();
        pane1.minHeightProperty().bind(name.heightProperty());
        Pane pane2 = new Pane();
        pane2.minHeightProperty().bind(name.heightProperty());
        Label label = new Label("Name:");
        Label label1 = new Label("Username:");
        Label label2 = new Label("Email");
        Label label3 = new Label("Phone number");
        Label label4 = new Label("Location");
        Label passwordLabel = new Label("Password");

        GridPane.setConstraints(name, 3, 0);
        GridPane.setColumnSpan(name, 3);
        GridPane.setConstraints(username, 3, 1);
        GridPane.setColumnSpan(username, 3);
        GridPane.setConstraints(password, 3, 2);
        GridPane.setColumnSpan(password, 3);
        GridPane.setConstraints(email, 3, 3);
        GridPane.setColumnSpan(email, 3);
        GridPane.setConstraints(phoneNumber, 3, 4);
        GridPane.setColumnSpan(phoneNumber, 3);
        GridPane.setConstraints(location, 3, 5);
        GridPane.setColumnSpan(location, 3);
        GridPane.setConstraints(label, 0, 0);
        GridPane.setColumnSpan(label, 3);
        GridPane.setConstraints(label1, 0, 1);
        GridPane.setColumnSpan(label1, 3);
        GridPane.setConstraints(passwordLabel, 0, 2);
        GridPane.setColumnSpan(passwordLabel, 3);
        GridPane.setConstraints(label2, 0, 3);
        GridPane.setColumnSpan(label2, 3);
        GridPane.setConstraints(label3, 0, 4);
        GridPane.setColumnSpan(label3, 3);
        GridPane.setConstraints(label4, 0, 5);
        GridPane.setColumnSpan(label4, 3);
        GridPane.setConstraints(cb1,0,6);
        GridPane.setColumnSpan(cb1, 3);
        GridPane.setConstraints(save, 3, 9);
        GridPane.setColumnSpan(save, 2);
        GridPane.setConstraints(cancel, 5, 9);
        GridPane.setColumnSpan(cancel, 2);

        root.getChildren().addAll(name, username, password, email, phoneNumber, location, passwordLabel, label, label1, label2, label3, label4, cb1, save, cancel, pane1, pane2);

        save.setOnAction(event -> {
            if(name.getText().isEmpty())
            {
                name.setStyle("-fx-prompt-text-fill: red");
                name.setPromptText("Name");
            }
            else if (name.getText().length() > 25) {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setHeaderText("Error");
                alert.setContentText("Name cannot be longer than 25 letters");
                alert.showAndWait();
            }
            else if(username.getText().isEmpty())
            {
                username.setStyle("-fx-prompt-text-fill: red");
                username.setPromptText("Username");
            }
            else if(username.getText().length() <5)
            {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setHeaderText("Error");
                alert.setContentText("Username should be between 5 and 15 characters");
                alert.showAndWait();
            }
            else if(username.getText().length() > 15)
            {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setHeaderText("Error");
                alert.setContentText("Username should be between 5 and 15 characters");
                alert.showAndWait();
            }
            else if(email.getText().isEmpty())
            {
                email.setStyle("-fx-prompt-text-fill: red");
                email.setPromptText("Email");
            }
            else if(email.getText().length() > 25)
            {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setHeaderText("Error");
                alert.setContentText("Email should be between 4 and 25 characters");
                alert.showAndWait();
            }
            else if(email.getText().length() < 4)
            {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setHeaderText("Error");
                alert.setContentText("Email should be between 4 and 25 characters");
                alert.showAndWait();
            }
            else if(phoneNumber.getText().isEmpty())
            {
                phoneNumber.setStyle("-fx-prompt-text-fill: red");
                phoneNumber.setPromptText("Phone number");
            }
            else if(phoneNumber.getText().length() < 3)
            {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setHeaderText("Error");
                alert.setContentText("Phone number should be between 3 and 12 characters");
                alert.showAndWait();
            }
            else if(phoneNumber.getText().length() > 12)
            {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setHeaderText("Error");
                alert.setContentText("Phone number should be between 3 and 12 characters");
                alert.showAndWait();
            }
            else if(location.getText().isEmpty())
            {
                location.setStyle("-fx-prompt-text-fill: red");
                location.setPromptText("Location");
            }
            else if(location.getText().length() < 8)
            {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setHeaderText("Error");
                alert.setContentText("Location should be no shorter than 8 characters");
                alert.showAndWait();
            }
            else if(password.getText().isEmpty())
            {
                password.setStyle("-fx-prompt-text-fill: red");
                password.setPromptText("Password");
            }
            else if(password.getText().length() < 5) {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setHeaderText("Error");
                alert.setContentText("Password should be between 5 and 15 characters");
                alert.showAndWait();;
            }
            else if(password.getText().length() > 15) {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setHeaderText("Error");
                alert.setContentText("Password should be between 5 and 15 characters");
                alert.showAndWait();
            }

            else {

                    controller.execute("addNewPharmacy");
                    add.close();
                    completeAction("Pharmacy added", successfulOperation);
            }

        });

        cancel.setOnAction(event -> add.close());

        Scene scene = new Scene(root, 400, 400);
        add.setScene(scene);
        add.show();
    }



    public void showStage() {
        adminMenu.show();
    }

    public Pharmacy getSelectedPharmacy() {
        Pharmacy pharmacy = (Pharmacy) tableViewPharmacy.getSelectionModel().getSelectedItem();
        return pharmacy;
    }

    public Pharmacy getEditedPharmacy() {
        Pharmacy pharmacy = getSelectedPharmacy();
        pharmacy.setName(name.getText());
        pharmacy.setUsername(username.getText());
        pharmacy.setEmail(email.getText());
        pharmacy.setPhoneNumber(phoneNumber.getText());
        pharmacy.setLocation(location.getText());
        pharmacy.setIsAdmin(cb1.isSelected());
        return pharmacy;
    }

    public Pharmacy getNewPharmacy() {
        Pharmacy pharmacy = new Pharmacy();
        pharmacy.setName(name.getText());
        pharmacy.setUsername(username.getText());
        pharmacy.setPassword(password.getText());
        pharmacy.setEmail(email.getText());
        pharmacy.setPhoneNumber(phoneNumber.getText());
        pharmacy.setLocation(location.getText());
        pharmacy.setIsAdmin(cb1.isSelected());
        return pharmacy;
    }



    public void completeAction(String whatHappened, Text text) {
        text.setText(whatHappened);
        Timer timer = new Timer();
        timer.schedule(new TimerTask() {
            public void run() {
                text.setText("");
                timer.cancel();
            }
        }, 3000);
    }

}
