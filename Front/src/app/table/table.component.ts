import { NgFor, NgIf } from '@angular/common';
import { Component, Input, Output, EventEmitter, OnChanges,SimpleChanges  } from '@angular/core';



@Component({
  selector: 'app-table',
  standalone: true,
 imports: [NgFor, NgIf],
  templateUrl: './table.component.html',
  styleUrl: './table.component.css'
})
export class TableComponent implements OnChanges {
  @Input() columns: string[] = [];
  @Input() data: any[] = [];
  @Input() action_columns: string[] = [];
  @Input() action_data: any[] = [];
  @Output() buttonClick = new EventEmitter<any>();
  @Input()
  top = "100px";
  @Input()
  left = "100px";

  onButtonClick(item: any, action: string) {
    this.buttonClick.emit({ item, action });
  }

  ngOnChanges(changes: SimpleChanges) {

  }
}
