import { Component ,  OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeadComponent } from '../head/head.component';
import { TableComponent } from '../../table/table.component';
import { PaginationComponent } from '../../pagination/pagination.component';
import { ActivatedRoute, RouterOutlet, RouterLink, Router, Params } from '@angular/router';
import { Observable } from "rxjs";
import { WarehousemanPageService, UsingProductsPlaces } from './warehouseman-page.service';

@Component({
  selector: 'app-hradmin-page',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HeadComponent, TableComponent, PaginationComponent],
  providers:[WarehousemanPageService],
  templateUrl: './warehouseman-page.component.html',
  styleUrl: './warehouseman-page.component.css'
})
export class WarehousemanPageComponent implements OnInit {

  columns: (string)[] = [];
  data: UsingProductsPlaces[] = [];
  current: number = 1;
  total: number = 0;
  name: string = "";
  constructor(private HRService: WarehousemanPageService, private router: Router, private route: ActivatedRoute)
  {
      this.columns = this.HRService.getColumns();
      route.queryParams.subscribe(
        (queryParam: any) => {
            this.name = queryParam['login'];
        }
    );
  }

  ngOnInit() {
    this.HRService.getUsingProductsPlaces().subscribe(
      (results) => {
        console.log(results);
        this.data = results.items;
        this.total = results.pages;
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    )
  }


  exitClickHandler(){
    this.router.navigate([""]);
  }

  addClickHandler(){

  }

  searchClickHandler(searchValue: string){
      //this.data = this.HRService.getUsingProductsPlaceById(searchValue);
  }




  public onGoTo(page: number): void {
    this.current = this.HRService.onGoTo(page);
    this.HRService.getUsingProductsPlaces().subscribe(
      (results) => {
        console.log(results);
        this.data = results.items;
        this.total = results.pages;
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    )
  }
  public onNext(page: number): void {
    if(this.current<this.total){
    this.current = this.HRService.onNext();
    this.HRService.getUsingProductsPlaces().subscribe(
      (results) => {
        console.log(results);
        this.data = results.items;
        this.total = results.pages;
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    )}
  }
  public onPrevious(page: number): void {
    if(this.current>1)
    {
      this.current = this.HRService.onPrevious();
      this.HRService.getUsingProductsPlaces().subscribe(
        (results) => {
          console.log(results);
          this.data = results.items;
          this.total = results.pages;
        },
        (error) => {
          console.error('Error fetching users:', error);
        }
      )
  }
  }

  action_columns = ['Delete'];
  action_data = [ { icon: '24_delete.svg', label: 'Delete' }];


  onButtonClick(event: any) {
    if(event.action.label == "Delete")
    {
      console.log(event.item.InventoryNumber);
      this.HRService.deleteUsingProductsPlaceById(event.item.InventoryNumber).subscribe(
        (error) => {
      console.error('Error fetching users:', error);
      }
      );
    }
    console.log(`Button clicked for item: ${event.item.Id} with action: ${event.action.label}`);
  }
}
