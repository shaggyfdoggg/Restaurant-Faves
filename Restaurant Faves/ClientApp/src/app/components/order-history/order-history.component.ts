import { Component } from '@angular/core';
import { Order } from 'src/app/models/order';
import { RestaurantFavesService } from 'src/app/service/restaurant-faves.service';


@Component({
  selector: 'app-order-history',
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.css']
})
export class OrderHistoryComponent {

  OrderListResult:Order[] = [];
  constructor(private _orderService:RestaurantFavesService) {}

  ngOnInit():void{
    this._orderService.GetOrder(null, null).subscribe((response: Order[]) => {
      console.log(response);
      this.OrderListResult = response;
    });
  }

  DeleteOrder(id:number):void{
    let target: number = this.OrderListResult.findIndex(o => o.id == id);
    this.OrderListResult.splice(target, 1);

    this._orderService.DeleteOrder(id).subscribe((response: Order) => {
      console.log(response);
    })
  }

  NewOrder(newOrder:Order){
    this._orderService.AddOrder(newOrder).subscribe((response:Order) => {
        console.log(response);
        this.OrderListResult.push(response);
    });
  }

}
