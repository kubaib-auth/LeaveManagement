import { Component } from '@angular/core';

@Component({
  selector: 'header-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent {
  notifications: string[] = ['Notification 1', 'Notification 2', 'Notification 3'];
  showNotifications: boolean = false;

  toggleNotifications() {
    this.showNotifications = !this.showNotifications;
  }
}
