import { AfterViewInit, Component, inject, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CustomerPrediction, CustomersService } from '../../core/services/customers.service';
import { MatDialog } from '@angular/material/dialog';
import { CustomerOrdersModalComponent } from './modals/customer-orders-modal/customer-orders-modal.component';
import { CreateOrderModalComponent } from './modals/create-order-modal/create-order-modal.component';


@Component({
  standalone: true,
  selector: 'app-customers',
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  templateUrl: './customers.component.html',
  styleUrl: './customers.component.scss'
})
export class CustomersComponent implements AfterViewInit {


  displayedColumns = ['customerName', 'lastOrderDate', 'nextPredictedOrder', 'actions'];
  // data: CustomerPrediction[] = [];
  dataSource = new MatTableDataSource<CustomerPrediction>();
  
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private customerService: CustomersService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.customerService.getPredictedOrders()
      .subscribe(response => {
        this.dataSource.data = response;
      });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.dataSource.filterPredicate = (data, filter) =>
      data.customerName.toLowerCase().includes(filter);
  }

  viewOrders(customer: CustomerPrediction) {
    console.log(customer);
    this.dialog.open(CustomerOrdersModalComponent, {
      width: '800px',
      data: {
        custId: customer.custId, // <-- debes incluir este campo en el DTO
        customerName: customer.customerName
      }
    });
  }

  
  
  newOrder(customer: CustomerPrediction): void {
    this.dialog.open(CreateOrderModalComponent, {
      width: '600px',
      data: { 
        custId: customer.custId ,
        customerName: customer.customerName
      }
    });  
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }


}