import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { NotificationAppServicesServiceProxy, UserNotificationDto } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'view-notification',
  templateUrl: './view-notification.component.html',
  styleUrls: ['./view-notification.component.css']
})
export class ViewNotificationComponent extends AppComponentBase  implements OnInit{
  notifications:UserNotificationDto[];
  constructor(
    injector: Injector,      
    private _notificationService:NotificationAppServicesServiceProxy
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.getAllNotifications();
    
  }
  getAllNotifications(){
    this._notificationService.getAllUserNotifications().subscribe(response=>{
      debugger
        this.notifications = response;   
    })
  }
  view(id:string){
    this._notificationService.viewNotification(id).subscribe(x=>{
         
    });
   
   }
  delete(id:string){
      this._notificationService.delete(id).subscribe(res=>{
       
      });
      this.getAllNotifications();
  }
}
