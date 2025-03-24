import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface Order {
  orderId: number;
  requiredDate: string;
  shippedDate: string | null;
  shipName: string;
  shipAddress: string;
  shipCity: string;
}

export interface CreateOrderRequest {
  custId: number;
  empId: number;
  shipperId: number;
  shipName: string;
  shipAddress: string;
  shipCity: string;
  shipRegion?: string;
  shipPostalCode?: string;
  shipCountry: string;
  orderDate: string;
  requiredDate: string;
  shippedDate?: string;
  freight: number;
  productId: number;
  unitPrice: number;
  qty: number;
  discount: number;
}

@Injectable({ providedIn: 'root' })
export class OrdersService {
  private readonly baseUrl = environment.apiUrl + '/orders';
  private readonly customerOrdersUrl = environment.apiUrl + '/customers';

  constructor(private http: HttpClient) {}

  getOrdersByCustomer(customerId: number): Observable<Order[]> {
    console.log(this.customerOrdersUrl);
    return this.http.get<Order[]>(`${this.customerOrdersUrl}/${customerId}/orders`);
  }

  createOrder(order: CreateOrderRequest): Observable<number> {
    return this.http.post<number>(this.baseUrl, order);
  }
}
