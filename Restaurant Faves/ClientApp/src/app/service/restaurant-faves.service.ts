import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Order } from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class RestaurantFavesService {

  constructor(private http:HttpClient, @Inject('BASE_URL') private baseUrl: string) { }


GetOrder(restaurant: string | null, orderAgain:boolean | null):Observable<Order[]>{
  const queryParams: any = {} as any;
  if (restaurant !== null)
  {
    queryParams.restaurant = restaurant;
  }
  if(orderAgain !== null)
  {
    queryParams.orderAgain = orderAgain;
  }

  return this.http.get<Order[]>(`${this.baseUrl}api/Order/`,{params: queryParams});
 
}

GetByID(id:number):Observable<Order[]>{
  return this.http.get<Order[]>(`${this.baseUrl}api/Order/${id}`);
}

AddOrder(newOrder:Order):Observable<Order>{
  return this.http.post<Order>(`${this.baseUrl}api/Order`, newOrder);
}

DeleteOrder(id:number):Observable<Order>{
  return this.http.delete<Order>(`${this.baseUrl}api/Order/${id}`);
}

UpdateOrder(id:number, newOrder:Order):Observable<Order>{
  return this.http.put<Order>(`${this.baseUrl}api/Order/${id}`, newOrder);
}

}