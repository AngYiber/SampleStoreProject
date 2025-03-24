import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface Employee {
  empId: number;
  fullName: string;
}

export interface Shipper {
  shipperId: number;
  companyName: string;
}

export interface Product {
  productId: number;
  productName: string;
}

@Injectable({ providedIn: 'root' })
export class FormDataService {
  constructor(private http: HttpClient) {}

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${environment.apiUrl}/employees`);
  }

  getShippers(): Observable<Shipper[]> {
    return this.http.get<Shipper[]>(`${environment.apiUrl}/shippers`);
  }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${environment.apiUrl}/products`);
  }
}
