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


  constructor(
    injector: Injector,      
    private _notificationService:NotificationAppServicesServiceProxy
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.getAllNotifications();
  }
  notifications: string[] = ['Notification 1', 'Notification 2', 'Notification 3'];
  showNotifications: boolean = false;
  notification : UserNotificationDto[];


  toggleNotifications() {
    this.showNotifications = !this.showNotifications;
  }

  getAllNotifications(){
    this._notificationService.getUserNotifications().subscribe(response=>{
        this.notification = response;
        console.log("Notification " , this.notification)
    })
  }
}
