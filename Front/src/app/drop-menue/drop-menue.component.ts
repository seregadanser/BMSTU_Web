import { Component, Input, EventEmitter, Output, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-drop-menue',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './drop-menue.component.html',
  styleUrl: './drop-menue.component.css'
})
export class DropMenueComponent implements OnInit{
@Input()
value1: string = "Инвентарные предметы";
@Input()
value2: string = "Места";
@Input()
selected: number = 1;

@Output() selectionChanged: EventEmitter<string> = new EventEmitter<string>();
  isValue1Selected: boolean = true;

  selectItem(item: string): void {
    this.isValue1Selected = item === 'value1';
    this.selectionChanged.emit(item); // Emit the selected value
  }

  ngOnInit(): void {
    //console.log(this.selected);
    if(this.selected == 1)
    {
      this.isValue1Selected = true;
    }
    else
    {
      this.isValue1Selected = false;
    }
  }
}
