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
import sun.misc.resources.Messages_it;

import java.util.List;
import java.util.Timer;
import java.util.TimerTask;
import java.util.function.UnaryOperator;
import java.util.regex.Pattern;


public class AdminMedicamentsMenu {
    private Stage medicamentsMenu;
    private FilteredList<Medicament> filteredMedicamentsData;
    private TableView tableViewMedicaments;
    private ObservableList<Medicament> data;
    private Stage add;
    private Stage addMedicaments;
    private Stage editMedicaments;
    private TextField name;
    private TextField price;
    private TextArea description;
    private ChoiceBox prescription;
    private Text successfulOperation;
    private MedicoController controller;
    private boolean canBeDeleted;

    public AdminMedicamentsMenu(MedicoController controller) {
        this.tableViewMedicaments = new TableView();
        this.medicamentsMenu = new Stage();
        this.medicamentsMenu = medicamentsMenu();
        this.controller = controller;
        this.canBeDeleted = false;
    }

    public void setMedicamentsItems(List<Medicament> medicamentList) {
        data = FXCollections.observableArrayList();
        for (int i = 0; i < medicamentList.size(); i++) {
            data.add(medicamentList.get(i));
        }
        this.filteredMedicamentsData = new FilteredList<>(data, p -> true);
        tableViewMedicaments.setItems(data);
        tableViewMedicaments.setItems(filteredMedicamentsData);
    }

    private Stage medicamentsMenu() {
        tableViewMedicaments.setEditable(true);
        medicamentsMenu.setResizable(false);
        VBox vBox = new VBox();
        HBox forImage = new HBox();
        forImage.setPadding(new Insets(0, 560, 0, 0));
        forImage.setAlignment(Pos.TOP_LEFT);
        HBox hBox = new HBox();
        hBox.setAlignment(Pos.TOP_RIGHT);
        Button addMedicament = new Button("Add");
        addMedicament.setPrefSize(100, 40);
        Button deleteMedicament = new Button("Delete");
        deleteMedicament.setPrefSize(100,40);
        hBox.setPadding(new Insets(20, 20, 20, 20));
        hBox.setSpacing(40);
        Button refreshButton = new Button("Refresh");
        refreshButton.setPrefSize(80,35);
        hBox.getChildren().addAll(forImage, addMedicament, deleteMedicament);
        HBox hBox1 = new HBox();
        hBox1.setAlignment(Pos.CENTER);
        HBox hBox2 = new HBox();
        hBox2.setAlignment(Pos.BOTTOM_RIGHT);
        hBox2.setPadding(new Insets(20, 20, 20, 20));

        addMedicament.setOnAction(event ->
        {
            addMedicaments();
        });

        deleteMedicament.setOnAction(event -> {
            controller.execute("checkIfMedicamentCanBeDeleted");

            if(tableViewMedicaments.getItems().isEmpty())
            {

            }
            else if(tableViewMedicaments.getSelectionModel().isEmpty())
            {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setTitle("Medicament has not been selected");
                alert.setHeaderText(null);
                alert.setContentText("Please select medicament to delete");
                alert.showAndWait();
            }
            else if(canBeDeleted == true)
            {
                controller.execute("deleteMedicament");
                completeAction("Medicament deleted", successfulOperation);
            }
            else if(canBeDeleted == false)
            {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setTitle("Medicament cannot be deleted");
                alert.setContentText("Medicament either belongs to order or is prescribed");
                alert.setHeaderText(null);
                alert.showAndWait();
            }
        });

        refreshButton.setOnAction(event -> {
            controller.execute("RefreshMedicaments");
        });

        TextField textField = new TextField();
        textField.setPromptText("Search by name");
        successfulOperation = new Text();
        successfulOperation.setStyle("-fx-font: 20 arial;");
        HBox hbox3 = new HBox();
        hbox3.setPadding(new Insets(0,0,0,300));
        hbox3.getChildren().addAll(refreshButton);
        hbox3.setSpacing(5);
        hBox2.getChildren().addAll(successfulOperation,hbox3, textField);
        hBox2.setSpacing(10);

        textField.setOnKeyReleased(event -> {
            filteredMedicamentsData.setPredicate(pharmacy -> pharmacy.getName().toLowerCase().contains(textField.getText().toLowerCase().trim()));
        });


        TableColumn nameCol = new TableColumn("Name");
        TableColumn idCol = new TableColumn("ID");
        TableColumn descriptionCol = new TableColumn("Description");
        TableColumn isPrescribedCol = new TableColumn("Is prescribed");
        TableColumn priceCol = new TableColumn("Price");


        nameCol.setCellValueFactory(new PropertyValueFactory<Medicament, String>("name"));
        idCol.setCellValueFactory(new PropertyValueFactory<Medicament, String>("id"));
        descriptionCol.setCellValueFactory(new PropertyValueFactory<Medicament, String>("description"));
        isPrescribedCol.setCellValueFactory(new PropertyValueFactory<Medicament, Boolean>("isPrescribed"));
        priceCol.setCellValueFactory(new PropertyValueFactory<Medicament, String>("price"));


        nameCol.setMinWidth(170);
        idCol.setMinWidth(50);
        descriptionCol.setMinWidth(620);
        isPrescribedCol.setMinWidth(50);
        priceCol.setMinWidth(50);




        tableViewMedicaments.getColumns().addAll(nameCol, idCol, descriptionCol, isPrescribedCol, priceCol);
        tableViewMedicaments.setMinHeight(480);
        tableViewMedicaments.setMinWidth(1010);
        hBox1.getChildren().add(tableViewMedicaments);

        vBox.getChildren().addAll(hBox, hBox1, hBox2);


        tableViewMedicaments.setOnMouseClicked(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                if (event.getClickCount() == 2) {
                    if (tableViewMedicaments.getSelectionModel().getSelectedItem() != null) {
                        editMedicament(tableViewMedicaments.getSelectionModel());
                    }
                }
            }
        });

        Scene scene = new Scene(vBox, 1000, 650);
        medicamentsMenu.setScene(scene);
        medicamentsMenu.setTitle("Medicaments menu");
        return medicamentsMenu;
    }




    public void addMedicaments()
    {
        addMedicaments = new Stage();
        GridPane root = new GridPane();
        root.setPadding(new Insets(20, 20, 20, 20));
        root.setVgap(10);
        root.setHgap(10);
        root.setAlignment(Pos.TOP_CENTER);
        name = new TextField();
        description = new TextArea();
        price = new TextField();
        prescription = new ChoiceBox(FXCollections.observableArrayList(
                "Yes", "No")
        );

        Pattern pattern = Pattern.compile("\\d*|\\d+\\.\\d*");
        TextFormatter formatter = new TextFormatter((UnaryOperator<TextFormatter.Change>) change -> {
            return pattern.matcher(change.getControlNewText()).matches() ? change : null;
        });
        price.setTextFormatter(formatter);

        Button save = new Button("Save");

        save.setPrefSize(70, 30);
        Button cancel = new Button("Cancel");
        cancel.setPrefSize(70, 30);
        Pane pane1 = new Pane();
        pane1.minHeightProperty().bind(name.heightProperty());
        Pane pane2 = new Pane();
        pane2.minHeightProperty().bind(name.heightProperty());
        Label label = new Label("Name:");
        Label label1 = new Label("Description:");
        Label label2 = new Label("Is prescribed");
        Label label3 = new Label("Price");

        GridPane.setConstraints(name, 3, 0);
        GridPane.setColumnSpan(name, 3);
        GridPane.setConstraints(description, 3, 1);
        GridPane.setColumnSpan(description, 3);
        GridPane.setConstraints(prescription, 3, 2);
        GridPane.setColumnSpan(prescription, 3);
        GridPane.setConstraints(price, 3, 3);
        GridPane.setColumnSpan(price, 3);
        GridPane.setConstraints(label,0,0);
        GridPane.setColumnSpan(label, 3);
        GridPane.setConstraints(label1, 0, 1);
        GridPane.setColumnSpan(label1, 3);
        GridPane.setConstraints(label2, 0, 2);
        GridPane.setColumnSpan(label2, 3);
        GridPane.setConstraints(label3, 0, 3);
        GridPane.setColumnSpan(label3, 3);

        GridPane.setConstraints(save, 3, 9);
        GridPane.setColumnSpan(save, 2);
        GridPane.setConstraints(cancel, 5, 9);
        GridPane.setColumnSpan(cancel, 2);

        root.getChildren().addAll(name, description, prescription, price, label, label1, label2, label3, save, cancel, pane1, pane2);

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
            else if(description.getText().isEmpty())
            {
                description.setStyle("-fx-prompt-text-fill: red");
                description.setPromptText("Description");
            }
            else if(prescription.getSelectionModel().isEmpty())
            {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setHeaderText("Error");
                alert.setContentText("Select either if medicament requires prescription or not");
                alert.showAndWait();
            }
            else if(price.getText().isEmpty())
            {
                price.setStyle("-fx-prompt-text-fill: red");
                price.setPromptText("99.99");
            }

            else {

                controller.execute("addNewMedicament");
                addMedicaments.close();
                completeAction("Medicament added", successfulOperation);

            }

        });

        cancel.setOnAction(event -> addMedicaments.close());

        Scene scene = new Scene(root, 400, 400);
        addMedicaments.setScene(scene);
        addMedicaments.show();
    }



    public void editMedicament(SelectionModel selectionModel)
    {
        controller.execute("checkIfMedicamentCanBeDeleted");
        Medicament medicament = (Medicament) selectionModel.getSelectedItem();
        editMedicaments = new Stage();
        GridPane root = new GridPane();
        root.setPadding(new Insets(20, 20, 20, 20));
        root.setVgap(10);
        root.setHgap(10);
        root.setAlignment(Pos.TOP_CENTER);
        name = new TextField();
        description = new TextArea();
        price = new TextField();
        prescription = new ChoiceBox(FXCollections.observableArrayList(
                "Yes", "No")
        );
        Pattern pattern = Pattern.compile("\\d*|\\d+\\.\\d*");
        TextFormatter formatter = new TextFormatter((UnaryOperator<TextFormatter.Change>) change -> {
            return pattern.matcher(change.getControlNewText()).matches() ? change : null;
        });
        price.setTextFormatter(formatter);

        Button save = new Button("Save");

        save.setPrefSize(70, 30);
        Button cancel = new Button("Cancel");
        cancel.setPrefSize(70, 30);
        Pane pane1 = new Pane();
        pane1.minHeightProperty().bind(name.heightProperty());
        Pane pane2 = new Pane();
        pane2.minHeightProperty().bind(name.heightProperty());
        Label label = new Label("Name:");
        Label label1 = new Label("Description:");
        Label label2 = new Label("Is prescribed");
        Label label3 = new Label("Price");

        GridPane.setConstraints(name, 3, 0);
        GridPane.setColumnSpan(name, 3);
        GridPane.setConstraints(description, 3, 1);
        GridPane.setColumnSpan(description, 3);
        GridPane.setConstraints(prescription, 3, 2);
        GridPane.setColumnSpan(prescription, 3);
        GridPane.setConstraints(price, 3, 3);
        GridPane.setColumnSpan(price, 3);
        GridPane.setConstraints(label,0,0);
        GridPane.setColumnSpan(label, 3);
        GridPane.setConstraints(label1, 0, 1);
        GridPane.setColumnSpan(label1, 3);
        GridPane.setConstraints(label2, 0, 2);
        GridPane.setColumnSpan(label2, 3);
        GridPane.setConstraints(label3, 0, 3);
        GridPane.setColumnSpan(label3, 3);

        GridPane.setConstraints(save, 3, 9);
        GridPane.setColumnSpan(save, 2);
        GridPane.setConstraints(cancel, 5, 9);
        GridPane.setColumnSpan(cancel, 2);

        root.getChildren().addAll(name, description, prescription, price, label, label1, label2, label3, save, cancel, pane1, pane2);

        name.setText(medicament.getName());
        description.setText(medicament.getDescription());
        if (medicament.getIsPrescribed() == true) {
            prescription.setValue("Yes");
        }
        else{
            prescription.setValue("No");
        }
        price.setText(String.valueOf(medicament.getPrice()));

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
            else if(description.getText().isEmpty())
            {
                description.setStyle("-fx-prompt-text-fill: red");
                description.setPromptText("Description");
            }
            else if(prescription.getSelectionModel().isEmpty())
            {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setHeaderText("Error");
                alert.setContentText("Select either if medicament requires prescription or not");
                alert.showAndWait();
            }
            else if(price.getText().isEmpty())
            {
                price.setStyle("-fx-prompt-text-fill: red");
                price.setPromptText("99.99");
            }
            else if(canBeDeleted == true)
            {
                controller.execute("editMedicament");
                editMedicaments.close();
                completeAction("Medicament edited", successfulOperation);
            }
            else if(canBeDeleted == false)
            {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setTitle("Medicament cannot be edited");
                alert.setContentText("Medicament either belongs to order or is prescribed");
                alert.setHeaderText(null);
                alert.showAndWait();
            }

        });
        cancel.setOnAction(event -> editMedicaments.close());

        Scene scene = new Scene(root, 400, 400);
        editMedicaments.setScene(scene);
        editMedicaments.show();
    }




    public void showStage() {
        medicamentsMenu.show();
    }


    public void setCanBeDeleted(boolean canBeDeleted) {
        this.canBeDeleted = canBeDeleted;
    }


    public Medicament getSelectedMedicament()
    {
        Medicament medicament = (Medicament) tableViewMedicaments.getSelectionModel().getSelectedItem();
        return medicament;
    }


    public Medicament getNewMedicament()
    {
        Medicament medicament = new Medicament();
        medicament.setName(name.getText());
        medicament.setDescription(description.getText());
        medicament.setPrice(Double.parseDouble(price.getText()));
        if (prescription.getValue() == "Yes")
        {
            medicament.setIsPrescribed(true);
        }
        else
        {
            medicament.setIsPrescribed(false);
        }
        return medicament;
    }

    public Medicament getEditedMedicament()
    {
        Medicament medicament = getSelectedMedicament();
        medicament.setName(name.getText());
        medicament.setDescription(description.getText());
        medicament.setPrice(Double.parseDouble(price.getText()));
        if (prescription.getValue() == "Yes")
        {
            medicament.setIsPrescribed(true);
        }
        else
        {
            medicament.setIsPrescribed(false);
        }
        return medicament;
    }


    public void completeAction(String whatHappened, Text text) {
        text.setText(whatHappened);
        Timer timer = new Timer();
        timer.schedule(new TimerTask() {
            public void run() {
                //Send add members email
                text.setText("");
                timer.cancel();
            }
        }, 3000);
    }



}

