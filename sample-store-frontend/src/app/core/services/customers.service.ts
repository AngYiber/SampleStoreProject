import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface CustomerPrediction {
  custId: number;
  customerName: string;
  lastOrderDate: string;
  nextPredictedOrder: string | null;
}

@Injectable({
  providedIn: 'root'
})
export class CustomersService {

  private readonly baseUrl = environment.apiUrl + '/customers';

  constructor(private http: HttpClient) {}

  getPredictedOrders(): Observable<CustomerPrediction[]> {
    return this.http.get<CustomerPrediction[]>(`${this.baseUrl}/predicted-orders`);
  }
}
