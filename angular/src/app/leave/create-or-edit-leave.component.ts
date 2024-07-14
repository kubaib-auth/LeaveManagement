import { Component, EventEmitter, Injector, Input, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { EmployeeDto, EnumLeave, LeaveAppServicesServiceProxy, LeaveCategoryDto, LeaveDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import * as moment from 'moment';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';
import { DatePipe, formatDate } from '@angular/common';
import { Console } from 'console';

@Component({
  selector: 'app-create-or-edit-leave',
  templateUrl: './create-or-edit-leave.component.html',
  styleUrls: ['./create-or-edit-leave.component.css'],
  animations:[]
})
export class CreateOrEditLeaveComponent extends AppComponentBase implements OnInit {
  @Input() id:number;
  @Input() checked1:Boolean; 
  @Input() fromDateInput: Date; // Define fromDateInput property
  @Input() toDateInput: Date;
  saving = false;
  @Output() onSave = new EventEmitter<any>();
  leave: LeaveDto = new LeaveDto();
  user: EmployeeDto[] = [];
  userid:number;
  selectedleaveCategory:number;
  leavetypeday:EnumLeave = undefined;
  currentUser:any= [];
  froDate: any;
  tDate: any;
  daysCalculate:any;
  leaveSpecificeId: any[] = [];
  Days:any;
  leaveCategorys=[];

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    private _modalService: BsModalService,
    private _userService: UserServiceProxy,
    private _leaveService:LeaveAppServicesServiceProxy,
    private route: ActivatedRoute,
    public datepipe:DatePipe
  ) {
    super(injector);
  }

  ngOnInit(): void {
   debugger
   this.getEmployee();
   this.getLeaveCategory();
   this.getCurrentUser();
   this.receiveId();

  }

  save(){
         this.saving = true;
         debugger
         if(this.leave.id){
          debugger  
          this.leave.userId
          this.leave.leaveCategoryId;
          this.leave.status = this.leavetypeday;  
          this.leave.days = this.calculateLeaveDays().toString();
          this._leaveService.createOrEdit(this.leave).subscribe(
          () => {
          this.notify.info(this.l('UpdatedSuccessfully'));
          this.bsModalRef.hide();
          this.onSave.emit();
          },
          () => {
          this.saving = false;
          }
      );
        }
        else{
          debugger
              this.leave.userId = this.currentUser.id;              
              this.leave.status = this.leavetypeday;              
              this.leave.days = this.calculateLeaveDays().toString();
              this._leaveService.createOrEdit(this.leave).subscribe((response)=> {
              this.notify.info(this.l('SavedSuccessfully'));
              this.bsModalRef.hide();
              this.onSave.emit();
               },
              () => {
              this.saving = false;
   })
     }
       
  }
  
  receiveId(){
    this._leaveService.getId(this.id).subscribe((response)=>{
      debugger
      this.leave = response;
      this.leavetypeday= this.leave.status;
      const fromDateString = this.leave.fromDate.toISOString();
      const toDateString = this.leave.toDate.toISOString();
      this.leave.fromDate = this.converToDateFormate(fromDateString);
      this.leave.toDate = this.converToDateFormate(toDateString);
      
  })
  }
  converToDateFormate(date: string): any{
      return new Date(date);
  }
   calculateLeaveDays(): number{
    this.froDate = this.leave.fromDate;
    this.tDate = this.leave.toDate;
    const fddDate = new Date(this.froDate);
    const tddDate = new Date(this.tDate);
    const Time = Math.abs(tddDate.getTime()-fddDate.getTime());
    this.daysCalculate = Time / (1000 * 3600 * 24)
    if(this.daysCalculate==0){
      this.daysCalculate= this.daysCalculate+1;
    }
    else if(this.daysCalculate>0){
      this.daysCalculate = this.daysCalculate + 1;
    }
    return this.daysCalculate;
   }
  // onCategorySelect(event) {  
  //   debugger
  //   const selectedCategoryId = event.value;
  //   console.log('Selected leave Category ID:', selectedCategoryId);
  // }
  getCurrentUser(){
     this._leaveService.getCurrentUser().subscribe((response)=>{
      this.currentUser= response;
     });
  }
  // onCatogorySelected(event: any){
  //   if (event && event.id) {
  //     this.selectedleaveCategory = event.id;
  //   }
  // }
  getEmployee(){
    this._leaveService.getAllUsers().subscribe((response)=>{
    this.user= response;
    })
  }
  getLeaveCategory(){
     this._leaveService.getAllLeaveCategory().subscribe((response)=>{
        debugger
        this.leaveCategorys= response;
        debugger
       })
  }
}


