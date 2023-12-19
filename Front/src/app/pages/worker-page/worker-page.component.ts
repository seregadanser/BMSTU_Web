import { Component ,  OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeadComponent } from '../head/head.component';
import { TableComponent } from '../../table/table.component';
import { PaginationComponent } from '../../pagination/pagination.component';
import { ActivatedRoute, RouterOutlet, RouterLink, Router, Params } from '@angular/router';
import { WorkerLookComposeWS, WorkerPageService, WorkerLookCompose } from './worker-page.service';

@Component({
  selector: 'app-admin-page',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HeadComponent, TableComponent, PaginationComponent],
  providers:[WorkerPageService],
  templateUrl: './worker-page.component.html',
  styleUrl: './worker-page.component.css'
})
export class WorkerPageComponent {
  columns: (string)[] = [];
  data: any [] = [];
  current: number = 1;
  total: number = 0;
  name: string = "";
  title: string = "Свободные предметы";

  isPlaceVisible =false;

  constructor(private AdminServise: WorkerPageService, private router: Router, private route: ActivatedRoute)
  {

      route.queryParams.subscribe(
        (queryParam: any) => {
            this.name = queryParam['login'];
        }
    );
  }

  ngOnInit() {
    this.columns = this.AdminServise.getItemColumns();
  /* this.AdminServise.getInventoryProducts().subscribe(
      (results) => {
        console.log(results);
        this.data = results.items;
        this.total = results.pages;
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    )*/
  }


  exitClickHandler(){
    this.router.navigate([""]);
  }

  addClickHandler(){

  }

  searchClickHandler(searchValue: string){
      //this.data = this.HRService.searchUser(searchValue);
  }

  changeOptionClickHandler(searchValue: string){
    //console.log(searchValue);
    this.current = this.AdminServise.onGoTo(1);
    if(searchValue == "value1")
    {
      this.isPlaceVisible = false;
      this.title = "Свободные предметы";
      this.columns = this.AdminServise.getItemColumns();
      this.setData();
    }
    else
    {
      this.isPlaceVisible = true;
      this.title = "Мои предметы";
      this.columns = this.AdminServise.getItemFColumns();
      this.setData();
    }
}


private setData()
{
  if(this.isPlaceVisible)
  {
    this.columns = this.AdminServise.getItemFColumns();
    this.AdminServise.getFreeInventoryProducts(true).subscribe(
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
  else
  {
    this.columns = this.AdminServise.getItemColumns();
    this.AdminServise.getFreeInventoryProducts(false).subscribe(
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



  public onGoTo(page: number): void {
    this.current = this.AdminServise.onGoTo(page);
    this.setData();
  }
  public onNext(page: number): void {
    if(this.current<this.total){
    this.current = this.AdminServise.onNext();
    this.setData();
  }
  }
  public onPrevious(page: number): void {
    if(this.current>1)
    {
      this.current = this.AdminServise.onPrevious();
      this.setData();
  }
  }

  actionPlace_columns = ['Delete'];
  actionPlace_data= [{ icon: '24_delete.svg', label: 'Delete' }];

  actionItem_columns = ['Add'];
  actionItem_data = [{ icon: '24_plus.svg', label: 'Add' }];

  onButtonClick(event: any) {
    if(event.action.label == "Add")
    {
      this.AdminServise.addFreeInventoryProduct(new WorkerLookComposeWS(
        event.item.Inventory_number,
        event.item.dateCome,
        event.item.dateProduction,
        event.item.Name
      )).subscribe(
        (error) => {
      console.error('Error fetching users:', error);
      }
      );
    }
    if(event.action.label == "Delete")
    {
      this.AdminServise.deleteFreeInventoryProductById(event.item.Inventory_number).subscribe(
        (error) => {
      console.error('Error fetching users:', error);
      }
      );
    }
    this.setData();
    console.log(`Button clicked for item: ${event.item.Id} with action: ${event.action.label}`);
  }
}
