import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { LeaveAppServicesServiceProxy } from '@shared/service-proxies/service-proxies';
@Component({
  selector: 'user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css'],
  
  animations: [appModuleAnimation()]
})
export class UserDashboardComponent extends AppComponentBase implements OnInit {
 userDetail:any;
  data: any;
  options: any;
  constructor(
    injector: Injector,       
    private _leaveService:LeaveAppServicesServiceProxy,
  ) {
    super(injector);
  }
  ngOnInit() {
    this.getDashboard()
    // const documentStyle = getComputedStyle(document.documentElement);
    // const textColor = documentStyle.getPropertyValue('--text-color');

    // this.data = {
    //     labels: ['A', 'B', 'C'],
    //     datasets: [
    //         {
    //             data: [540, 325, 702],
    //             backgroundColor: [documentStyle.getPropertyValue('--blue-500'), documentStyle.getPropertyValue('--yellow-500'), documentStyle.getPropertyValue('--green-500')],
    //             hoverBackgroundColor: [documentStyle.getPropertyValue('--blue-400'), documentStyle.getPropertyValue('--yellow-400'), documentStyle.getPropertyValue('--green-400')]
    //         }
    //     ]
    // };

    // this.options = {
    //     plugins: {
    //         legend: {
    //             labels: {
    //                 usePointStyle: true,
    //                 color: textColor
    //             }
    //         }
    //     }
    // };
}
getDashboard(){
    this._leaveService.userDashboard().subscribe(response=>{
        debugger
        this.userDetail= response;
    })
 }
}
