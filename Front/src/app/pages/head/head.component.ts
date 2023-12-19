import { Component, Input, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '../../button/button.component';
import { InputComponent } from '../../input/input.component';
import { DropMenueComponent } from '../../drop-menue/drop-menue.component';

@Component({
  selector: 'app-head',
  standalone: true,
  imports: [CommonModule, ButtonComponent, InputComponent, DropMenueComponent],
  templateUrl: './head.component.html',
  styleUrl: './head.component.css'
})
export class HeadComponent {

  isDropVisibleflag: boolean = false;
  whatSelected = 1;


  @Input() Name = "Ежжж";
  @Input() pageName = "Кролик";
  @Input() DropV1 = "Хомяк";
  @Input() DropV2 = "Мыш.";

  @Output() btnExitClick: EventEmitter<void> = new EventEmitter<void>();
  @Output() btnAddClick: EventEmitter<void> = new EventEmitter<void>();
  @Output() btnDropClick: EventEmitter<void> = new EventEmitter<void>();
  @Output() intEnter: EventEmitter<string> = new EventEmitter<string>();
  @Output() handleSelectionChange: EventEmitter<string> = new EventEmitter<string>();

  BtnExitClick()
  {
    this.btnExitClick.emit();
  }

  BtnAddClick()
  {
    this.btnAddClick.emit();
  }

  BtnDropClick()
  {
    //this.btnDropClick.emit();
    if(this.isDropVisibleflag)
    {
      this.isDropVisibleflag = false;
    }
    else
    {
      this.isDropVisibleflag = true;
    }
  }

  SearchClick(event: string)
  {
    this.intEnter.emit(event);
  }

  HandleSelectionChange(event: string)
  {
    if(event == "value1")
      this.whatSelected = 1;
    else
      this.whatSelected = 2;
    
    this.isDropVisibleflag = false;
    //console.log(this.isDropVisibleflag);
    this.handleSelectionChange.emit(event);
  }

  @Input()
  set isExitVisible(value: boolean | string) {
    this._isExitVisible = value !== 'false';
  }
  get isExitVisible(): boolean {
    return this._isExitVisible;
  }
  private _isExitVisible = false;

  @Input()
  set isHeadVisible(value: boolean | string) {
    this._isHeadVisible = value !== 'false';
  }
  get isHeadVisible(): boolean {
    return this._isHeadVisible;
  }
  private _isHeadVisible = false;

  @Input()
  set isSearchVisible(value: boolean | string) {
    this._isSearchVisible = value !== 'false';
  }
  get isSearchVisible(): boolean {
    return this._isSearchVisible;
  }
  private _isSearchVisible = false;


  @Input()
  set isAddVisible(value: boolean | string) {
    this._isAddVisible = value !== 'false';
  }
  get isAddVisible(): boolean {
    return this._isAddVisible;
  }
  private _isAddVisible = false;


  @Input()
  set isNameVisible(value: boolean | string) {
    this._isNameVisible = value !== 'false';
  }
  get isNameVisible(): boolean {
    return this._isNameVisible;
  }
  private _isNameVisible = false;


  @Input()
  set isDropVisible(value: boolean | string) {
    this._isDropVisible = value !== 'false';
  }
  get isDropVisible(): boolean {
    return this._isDropVisible;
  }
  private _isDropVisible = false;

}
