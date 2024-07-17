import { Component, Injector, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { NotificationAppServicesServiceProxy, UserNotificationDto } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'header-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent extends AppComponentBase  implements OnInit{
  notifications: string[] = ['Notification 1', 'Notification 2', 'Notification 3'];
  showNotifications: boolean = false;
  notification : UserNotificationDto[];
  ShowThreeNotify: any[] = [];
  constructor(
    injector: Injector,      
    private _notificationService:NotificationAppServicesServiceProxy
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.getAllNotifications();
  }
 


  toggleNotifications() {
    this.showNotifications = !this.showNotifications;
  }

  getAllNotifications(){
    this._notificationService.getUserNotifications().subscribe(response=>{
      
        this.notification = response;   
        this.ShowThreeNotify=this.notification.slice(0,3);    
    })
  }
  view(id:string){
      this._notificationService.viewNotification(id).subscribe(x=>{
           
      });
      
  }
}
