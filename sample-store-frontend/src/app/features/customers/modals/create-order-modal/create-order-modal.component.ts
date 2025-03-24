import {
  Component, inject, OnInit
} from '@angular/core';
import {
  FormBuilder, FormGroup, Validators, ReactiveFormsModule
} from '@angular/forms';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormDataService, Employee, Shipper, Product } from '../../../../core/services/form-data.service';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { Observable } from 'rxjs';
import { OrdersService } from '../../../../core/services/orders.service';

@Component({
  standalone: true,
  selector: 'app-create-order-modal',
  templateUrl: './create-order-modal.component.html',
  styleUrl: './create-order-modal.component.scss',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule
  ]
})
export class CreateOrderModalComponent implements OnInit {
  private fb = inject(FormBuilder);
  private formDataService = inject(FormDataService);
  private dialogRef = inject(MatDialogRef<CreateOrderModalComponent>);
  data = inject(MAT_DIALOG_DATA) as { custId: number; customerName: string };

  constructor(private ordersService: OrdersService){

  }

  form: FormGroup = this.fb.group({
    empId: [null, Validators.required],
    shipperId: [null, Validators.required],
    shipName: ['', Validators.required],
    shipAddress: ['', Validators.required],
    shipCity: ['', Validators.required],
    shipCountry: ['', Validators.required],
    orderDate: [new Date(), Validators.required],
    requiredDate: [new Date(), Validators.required],
    shippedDate: [null],
    freight: [0, Validators.required],
    productId: [null, Validators.required],
    unitPrice: [0, Validators.required],
    qty: [1, Validators.required],
    discount: [0]
  });

  employees$!: Observable<Employee[]>;
  shippers$!: Observable<Shipper[]>;
  products$!: Observable<Product[]>;

  ngOnInit(): void {
    this.employees$ = this.formDataService.getEmployees();
    this.shippers$ = this.formDataService.getShippers();
    this.products$ = this.formDataService.getProducts();
  }

  save(): void {
    if (this.form.invalid) return;

    const raw = this.form.value;

    const payload = {
      ...raw,
      custId: this.data.custId,
      orderDate: raw.orderDate?.toISOString(),
      requiredDate: raw.requiredDate?.toISOString(),
      shippedDate: raw.shippedDate ? raw.shippedDate.toISOString() : null
    };

    console.log(payload);

    this.ordersService.createOrder(payload).subscribe(() => {
      this.dialogRef.close(true);
    });
  }

  close(): void {
    this.dialogRef.close();
  }
}
