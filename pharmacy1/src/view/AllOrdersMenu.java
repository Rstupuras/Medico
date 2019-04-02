package view;

import controller.MedicoController;
import domain.model.Order;
import domain.model.OrderItem;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.collections.transformation.FilteredList;
import javafx.event.EventHandler;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
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

import static java.awt.SystemColor.text;

public class AllOrdersMenu {
    private Stage allOrdersMenu;
    private FilteredList<Order> filteredAllOrdersData;
    private TableView tableViewAllOrders;
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

    public AllOrdersMenu(MedicoController controller) {
        this.tableViewAllOrders = new TableView();
        this.allOrdersMenu = new Stage();
        this.allOrdersMenu = allOrdersMenu();
        this.controller=controller;

    }

    public void setAllOrderItems(Collection<Order> orderList) {
        List<Order> orders = (List<Order>) orderList;
        data = FXCollections.observableArrayList();
        for (int i = 0; i < orders.size(); i++) {
            data.add(orders.get(i));
        }
        this.filteredAllOrdersData = new FilteredList<>(data, p -> true);
        tableViewAllOrders.setItems(data);
        tableViewAllOrders.setItems(filteredAllOrdersData);
    }

    public Stage allOrdersMenu() {
        tableViewAllOrders.setEditable(true);
        allOrdersMenu.setResizable(false);
        VBox vBox = new VBox();
        HBox hBox = new HBox();
        hBox.setAlignment(Pos.TOP_RIGHT);
        Button refreshButton = new Button("Refresh");
        hBox.setPadding(new Insets(20, 20, 20, 20));
        hBox.setSpacing(40);

        hBox.getChildren().addAll();
        HBox hBox1 = new HBox();
        hBox1.setAlignment(Pos.CENTER);
        HBox hBox2 = new HBox();
        hBox2.setAlignment(Pos.BOTTOM_RIGHT);
        hBox2.setPadding(new Insets(20, 20, 20, 20));


        refreshButton.setOnAction(event -> {
            controller.execute("Refresh");
        });



        TextField textField = new TextField();
        textField.setPromptText("Search by order number");
        saveSuccessful = new Text();
        saveSuccessful.setStyle("-fx-font: 20 arial;");
        hBox2.getChildren().addAll(saveSuccessful,refreshButton, textField);
        hBox2.setSpacing(20);
        textField.setOnKeyReleased(event -> {
            filteredAllOrdersData.setPredicate(order -> order.getOrderNumber().contains(textField.getText().toLowerCase().trim()));
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

        tableViewAllOrders.getColumns().addAll(orderIdCol, medicamentsCol, orderNumberCol, patientIdCol, statusCol);
        tableViewAllOrders.setMinHeight(480);
        tableViewAllOrders.setMinWidth(1010);
        hBox1.getChildren().add(tableViewAllOrders);

        vBox.getChildren().addAll(hBox, hBox1, hBox2);


        Scene scene = new Scene(vBox, 1000, 650);
        allOrdersMenu.setScene(scene);
        allOrdersMenu.setTitle("All orders");
        return allOrdersMenu;

    }

    public Order getSelectedOrder() {
        Order order = (Order) tableViewAllOrders.getSelectionModel().getSelectedItem();
        return order;
    }

    public void showStage() {
        allOrdersMenu.show();
    }



}
