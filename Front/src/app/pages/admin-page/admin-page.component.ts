import { Component ,  OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeadComponent } from '../head/head.component';
import { TableComponent } from '../../table/table.component';
import { PaginationComponent } from '../../pagination/pagination.component';
import { ActivatedRoute, RouterOutlet, RouterLink, Router, Params } from '@angular/router';
import { AdminCompose, AdminPageService, Place } from './admin-page.service';

@Component({
  selector: 'app-admin-page',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HeadComponent, TableComponent, PaginationComponent],
  providers:[AdminPageService],
  templateUrl: './admin-page.component.html',
  styleUrl: './admin-page.component.css'
})
export class AdminPageComponent {
  columns: (string)[] = [];
  data: any [] = [];
  current: number = 1;
  total: number = 0;
  name: string = "";
  title: string = "Инвентарные предметы";

  isPlaceVisible =false;

  constructor(private AdminServise: AdminPageService, private router: Router, private route: ActivatedRoute)
  {

      route.queryParams.subscribe(
        (queryParam: any) => {
            this.name = queryParam['login'];
        }
    );
  }

  ngOnInit() {
    this.columns = this.AdminServise.getItemColumns();
    this.AdminServise.getInventoryProducts().subscribe(
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
if(this.isPlaceVisible)
{
  this.router.navigate(['admin/addPlace']);
}
else{
  this.router.navigate(['admin/addInv']);
}
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
      this.title = "Инвентарные";
      this.columns = this.AdminServise.getItemColumns();
      this.setData();
    }
    else
    {
      this.isPlaceVisible = true;
      this.title = "Места";
      this.columns = this.AdminServise.getPlacesColumns();
      this.setData();
    }
}


private setData()
{
  if(this.isPlaceVisible)
  {
    this.columns = this.AdminServise.getPlacesColumns();
    this.AdminServise.getPlaces().subscribe(
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
    this.AdminServise.getInventoryProducts().subscribe(
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

  actionItem_columns = ['Delete'];
  actionItem_data = [{ icon: '24_delete.svg', label: 'Delete' }];

  actionPlace_columns = ['Edit', 'Delete'];
  actionPlace_data = [{ icon: '24_highlight.svg', label: 'Edit' }, { icon: '24_delete.svg', label: 'Delete' }];

  onButtonClick(event: any) {
    if(event.action.label == "Edit")
    {
      const queryParams = {
        inputValue1: event.item.NumberStay,
        inputValue2: event.item.NumberLayer,
        inputValue3: event.item.Size,
      };
      this.router.navigate(['admin/editPlace', event.item.Id], { queryParams });

    }
    if(event.action.label == "Delete")
    {
      if(this.isPlaceVisible)
      {
        this.AdminServise.deletePlaceById(event.item.Id).subscribe(
                (error) => {
            console.error('Error fetching users:', error);
          }
        );}
      else
        this.AdminServise.deleteInventoryProductById(event.item.InventoryNumber).subscribe(
          (error) => {
      console.error('Error fetching users:', error);
    }
  );
      //this.HRService.deleteUser(event.Item.Id);
    }
    this.setData();
    console.log(`Button clicked for item: ${event.item.Id} with action: ${event.action.label}`);
  }
}
