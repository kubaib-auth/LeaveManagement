import { Component, Injector, Input, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalRef,BsModalService } from 'ngx-bootstrap/modal';
import { CreateOrEditLeaveComponent } from './create-or-edit-leave.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {  EmployeeDto, LeaveAppServicesServiceProxy, LeaveDto,  } from '@shared/service-proxies/service-proxies';
import { LazyLoadEvent } from 'primeng/api';
import { ApproveOrRejectedleavComponent } from './approve-or-rejectedleav.component';
import { PermissionCheckerService } from 'abp-ng2-module';

@Component({
  selector: 'app-leave',
  templateUrl: './leave.component.html',
  styleUrls: ['./leave.component.css'],
  
  animations: [appModuleAnimation()]
})
export class LeaveComponent extends AppComponentBase implements OnInit {
  checked1: boolean = false;
  leaveSpecificeId: any[] = [];
  leave: LeaveDto = new LeaveDto();
  @ViewChild(CreateOrEditLeaveComponent) getLeave :CreateOrEditLeaveComponent;
  
  //userAsignLeave:LeaveTypeBalanceDto =new LeaveTypeBalanceDto();
  alleave: any[] = [];
  employeeOptions:EmployeeDto[];
  leaveFiterId:number;
  loading: boolean = false; 
  fromDateInput:Date;
  toDateInput:Date;
  filter='';
  filterText='';
  categoryNameFilter='';
  sorting='';
  skipCount:number;
  maxResultCount:number;
  userAsignLeaveCas:any;
  userAsignLeaveAnn:any;
  userAsignLeaveSic:any;
  userBaLeaveCas:any;
  userBaLeaveAnn:any;
  userBaLeaveSic:any;

b:any;
    constructor(
      injector: Injector,      
      public bsModalRef: BsModalRef,
      private _modalService: BsModalService,
      private _leaveService:LeaveAppServicesServiceProxy,
      private _permissionChecker: PermissionCheckerService
    ) {
      super(injector);
    }
  
    ngOnInit() { 
      this.getLeaveAll();
      this.getuser();
    } 
   
     getLeaveAll(event?: LazyLoadEvent){ 
      debugger
      this._leaveService.getAll(  
       this.filter,
       this.filterText,
       this.categoryNameFilter,
       this.leaveFiterId === null? this.leaveFiterId = undefined: this.leaveFiterId,
       this.sorting,
        this.skipCount,
        this.maxResultCount,
       ).subscribe((result) => {
        debugger
          this.alleave = result.items; 
          debugger
          if (this.alleave.length > 0) {
            this.userAsignLeaveCas = this.alleave[0].casualLeaveAsign;
            this.userAsignLeaveAnn = this.alleave[0].annualLeaveAsign;
            this.userAsignLeaveSic = this.alleave[0].sickLeaveAsign;
            this.userBaLeaveCas = this.alleave[0].casualLeaveBalance;
            this.userBaLeaveAnn = this.alleave[0].annualLeaveBalance;
            this.userBaLeaveSic = this.alleave[0].sickLeaveBalance;
          } 

            // this._leaveService.getBalance().subscribe((response)=>{
            //  this.userAsignLeave= response;
            // });
        
        });
       
     }

    createUser(id?:number):void{
           this.checked1=true;
           const createOrEditLeaveDialog:BsModalRef = this._modalService.show(
            CreateOrEditLeaveComponent,{
              class:'modal-lg',
              initialState:{
                id:id || null,              
              },
            }
         );
         createOrEditLeaveDialog.content.onSave.subscribe((result)=>{
             
             this.getLeaveAll();
          });
    }
    leaveApproval(id?:number):void{
        const approveOrRejectedDialog : BsModalRef = this._modalService.show(
          ApproveOrRejectedleavComponent,{
            class:'modal-lg',
            initialState:{
                  id:id || null,
            },
          }
        );
    }
   
     getuser(){
     this._leaveService.getAllUsers().subscribe((response)=>{
        debugger
        this.employeeOptions= response;
        debugger
       })
  }
}
