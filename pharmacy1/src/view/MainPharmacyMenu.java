package view;

import controller.MedicoController;
import domain.model.Medicament;
import domain.model.Order;
import domain.model.OrderItem;
import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
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
import sun.font.TextLabel;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;


public class MainPharmacyMenu {
    private Stage mainMenu;
    private FilteredList<Order> filteredUndoneOrderData;
    private TableView tableViewUndoneOrders;
    private ObservableList<Order> data;
    private Stage informationEdit;
    private TextLabel orderId;
    private TextLabel medicaments;
    private TextLabel price;
    private TextLabel patientId;
    private TextLabel patientName;
    private TextLabel status;
    private Text saveSuccessful;

    private MedicoController controller;

    public MainPharmacyMenu(MedicoController controller) {
        this.tableViewUndoneOrders = new TableView();
        this.mainMenu = new Stage();
        this.mainMenu = mainMenu();
        this.controller = controller;

    }

    public void setUndoneOrderItems(Collection<Order> orderList) {
        List<Order> orders = (List<Order>) orderList;
        data = FXCollections.observableArrayList();
        for (int i = 0; i < orders.size(); i++) {
            data.add(orders.get(i));
        }
        this.filteredUndoneOrderData = new FilteredList<>(data, p -> true);
        tableViewUndoneOrders.setItems(data);
        tableViewUndoneOrders.setItems(filteredUndoneOrderData);
    }

    public Stage mainMenu() {
        tableViewUndoneOrders.setEditable(true);
        mainMenu.setResizable(false);
        VBox vBox = new VBox();
        HBox hBox = new HBox();
        hBox.setAlignment(Pos.TOP_RIGHT);
        Button refreshButton = new Button("Refresh");
        hBox.setPadding(new Insets(20, 20, 20, 20));
        hBox.setSpacing(40);
        Button allOrdersButton = new Button("All Orders");
        allOrdersButton.setPrefSize(100, 40);
        hBox.getChildren().addAll(allOrdersButton);
        HBox hBox1 = new HBox();
        hBox1.setAlignment(Pos.CENTER);
        HBox hBox2 = new HBox();
        hBox2.setAlignment(Pos.BOTTOM_RIGHT);
        hBox2.setPadding(new Insets(20, 20, 20, 20));


        refreshButton.setOnAction(event -> {
            controller.execute("Refresh");

        });

        allOrdersButton.setOnAction(event -> {
            controller.execute("allOrdersMenu");

        });



        mainMenu.setOnCloseRequest(event -> System.exit(0));


        TextField textField = new TextField();
        textField.setPromptText("Search by order number");
        saveSuccessful = new Text();
        saveSuccessful.setStyle("-fx-font: 20 arial;");
        hBox2.getChildren().addAll(saveSuccessful,refreshButton, textField);
        hBox2.setSpacing(20);
        textField.setOnKeyReleased(event -> {
            filteredUndoneOrderData.setPredicate(order -> order.getOrderNumber().contains(textField.getText().toLowerCase().trim()));
        });


        TableColumn orderIdCol = new TableColumn("Order ID");
        TableColumn medicamentsCol = new TableColumn("Medicaments");
        TableColumn orderNumberCol = new TableColumn("Order number");
        TableColumn patientIdCol = new TableColumn("Patient ID");
        TableColumn statusCol = new TableColumn("Status");

        medicamentsCol.setMinWidth(300);
        orderNumberCol.setMinWidth(150);
        statusCol.setMinWidth(150);

        orderIdCol.setCellValueFactory(new PropertyValueFactory<Order, Integer>("id"));
        medicamentsCol.setCellValueFactory(new PropertyValueFactory<Order, ArrayList<OrderItem>>("items"));
        orderNumberCol.setCellValueFactory(new PropertyValueFactory<List<OrderItem>, Double>("orderNumber"));
        patientIdCol.setCellValueFactory(new PropertyValueFactory<Order, Integer>("patientID"));
        statusCol.setCellValueFactory(new PropertyValueFactory<Order, String>("status"));


        tableViewUndoneOrders.getColumns().addAll(orderIdCol, medicamentsCol, orderNumberCol, patientIdCol, statusCol);
        tableViewUndoneOrders.setMinHeight(480);
        tableViewUndoneOrders.setMinWidth(1010);
        hBox1.getChildren().add(tableViewUndoneOrders);

        vBox.getChildren().addAll(hBox, hBox1, hBox2);

        tableViewUndoneOrders.setOnMouseClicked(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                if (event.getClickCount() == 2) {
                    if (tableViewUndoneOrders.getSelectionModel().getSelectedItem() != null)
                        showInformation(tableViewUndoneOrders.getSelectionModel());
                }
            }
        });


        Scene scene = new Scene(vBox, 1000, 650);
        mainMenu.setScene(scene);
        mainMenu.setTitle("Undone orders");
        return mainMenu;

    }
    public Order getSelectedOrderToChangeStatus()
    {
        Order order = (Order) tableViewUndoneOrders.getSelectionModel().getSelectedItem();
        return order;
    }

    public void showStage() {
        mainMenu.show();
    }

    public void showInformation(SelectionModel selectionModel) {

        Order order = (Order) selectionModel.getSelectedItem();
        informationEdit = new Stage();
        GridPane root = new GridPane();
        root.setPadding(new Insets(20, 20, 20, 20));
        root.setVgap(10);
        root.setHgap(10);
        root.setAlignment(Pos.TOP_CENTER);

        Button setDone = new Button("Complete Order");
        setDone.setPrefSize(150, 30);
        Button setDenied = new Button("Deny Order");
        setDenied.setPrefSize(150,30);
        Button cancel = new Button("Cancel");
        cancel.setPrefSize(150, 30);
        Pane pane1 = new Pane();
        pane1.minHeightProperty().setValue(50);
        Pane pane2 = new Pane();
        pane2.minHeightProperty().setValue(50);
        Label label = new Label("Order ID : ");
        Label label1 = new Label("Medicaments : ");
        Label label3 = new Label("Patient ID : ");
        Label label4 = new Label("Order number : ");
        Label label5 = new Label("Status : ");

        HBox hBox0 = new HBox();
        Text text0 = new Text(String.valueOf(order.getId()));
//        hBox0.getChildren().addAll(label, text0);

        HBox hBox1 = new HBox();
        Text text1 = new Text(order.getItems().toString());
        hBox1.getChildren().addAll(label1, text1);
        hBox1.maxWidthProperty().setValue(300);

        HBox hBox2 = new HBox();
        Text text2 = new Text(String.valueOf(order.getId()));
        hBox2.getChildren().addAll(label, text2);

        HBox hBox3 = new HBox();
        Text text3 = new Text(String.valueOf(order.getPatientID()));
        hBox3.getChildren().addAll(label3, text3);

        HBox hBox4 = new HBox();
        Text text4 = new Text(order.getOrderNumber());
        hBox4.getChildren().addAll(label4, text4);

        HBox hBox5 = new HBox();
        Text text5 = new Text(order.getStatus());
        hBox5.getChildren().addAll(label5, text5);
        hBox5.setPadding(new Insets(0,0,20,0));


        HBox hBox6 = new HBox();
        hBox6.setSpacing(10);
        hBox6.getChildren().addAll(setDone, setDenied, cancel);


        VBox vBox0 = new VBox();
        vBox0.getChildren().addAll(hBox0,hBox1, hBox2, hBox3, hBox4, hBox5, hBox6);
        root.getChildren().add(vBox0);
        vBox0.setSpacing(10);

        setDone.setOnAction(event -> {

            controller.execute("setOrderCompleted");
            informationEdit.close();
        });

        setDenied.setOnAction(event -> {

            controller.execute("setOrderDenied");
            informationEdit.close();
        });


        cancel.setOnAction(event -> informationEdit.close());

        Scene scene = new Scene(root, 500, 400);
        informationEdit.setScene(scene);
        informationEdit.show();

    }


}
