import { Component ,  OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeadComponent } from '../head/head.component';
import { TableComponent } from '../../table/table.component';
import { PaginationComponent } from '../../pagination/pagination.component';
import { HradminPageService, MyWorker } from './hradmin-page.service';
import { ActivatedRoute, RouterOutlet, RouterLink, Router, Params } from '@angular/router';
import { Observable } from "rxjs";

@Component({
  selector: 'app-hradmin-page',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HeadComponent, TableComponent, PaginationComponent],
  providers:[HradminPageService],
  templateUrl: './hradmin-page.component.html',
  styleUrl: './hradmin-page.component.css'
})
export class HradminPageComponent implements OnInit {

  columns: (string)[] = [];
  data: MyWorker[] = [];
  current: number = 1;
  total: number = 0;
  name: string = "";
  constructor(private HRService: HradminPageService, private router: Router, private route: ActivatedRoute)
  {
      this.columns = this.HRService.getColumns();
      route.queryParams.subscribe(
        (queryParam: any) => {
            this.name = queryParam['login'];
        }
    );
  }

  ngOnInit() {
    this.HRService.getUsers().subscribe(
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
    this.router.navigate(['hradmin/addUser']);
  }

  searchClickHandler(searchValue: string){
      this.data = this.HRService.searchUser(searchValue);
  }




  public onGoTo(page: number): void {
    this.current = this.HRService.onGoTo(page);
    this.HRService.getUsers().subscribe(
      (results) => {
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
    this.HRService.getUsers().subscribe(
      (results) => {
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
    this.HRService.getUsers().subscribe(
      (results) => {
        this.data = results.items;
        this.total = results.pages;
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    )
  }
  }

  action_columns = ['Edit', 'Delete'];
  action_data = [{ icon: '24_highlight.svg', label: 'Edit' }, { icon: '24_delete.svg', label: 'Delete' }];


  onButtonClick(event: any) {
    if(event.action.label == "Edit")
    {
      const queryParams = {
        inputValue1: event.item.Login,
        inputValue2: event.item.Name,
        inputValue3: event.item.SecondName,
        inputValue4: event.item.Position,
        inputValue5: event.item.DateOfBirthday,
      };
      this.router.navigate(['hradmin/editUser', event.item.Id], { queryParams });
    }
    if(event.action.label == "Delete")
    {
      this.HRService.deleteUser(event.Item.Id);
    }
    console.log(`Button clicked for item: ${event.item.Id} with action: ${event.action.label}`);
  }
}
