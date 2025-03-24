import { Component, Inject, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table';
import { OrdersService } from '../../../../core/services/orders.service';
import { MatButtonModule } from '@angular/material/button';


interface Order {
  orderId: number;
  requiredDate: string;
  shippedDate: string | null;
  shipName: string;
  shipAddress: string;
  shipCity: string;
}

@Component({
  standalone: true,
  selector: 'app-customer-orders-modal',
  imports: [CommonModule, MatDialogModule, MatTableModule, MatButtonModule],
  templateUrl: './customer-orders-modal.component.html',
  styleUrl: './customer-orders-modal.component.scss'
})
export class CustomerOrdersModalComponent {
  private readonly dialogRef = inject(MatDialogRef<CustomerOrdersModalComponent>);
  readonly data = inject(MAT_DIALOG_DATA) as { custId: number; customerName: string };

  orders: Order[] = [];
  displayedColumns = ['orderId', 'requiredDate', 'shippedDate', 'shipName', 'shipAddress', 'shipCity'];

  constructor(private ordersService: OrdersService) {}

  ngOnInit(): void {
    this.ordersService.getOrdersByCustomer(this.data.custId)
      .subscribe(orders => this.orders = orders);
  }

  close(): void {
    this.dialogRef.close();
  }
}
