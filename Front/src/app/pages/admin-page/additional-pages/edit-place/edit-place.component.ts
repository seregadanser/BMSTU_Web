import { Component, ViewChildren, QueryList, OnInit, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from "rxjs";
import { ActivatedRoute, Router, Params } from '@angular/router';
import { InputComponent } from '../../../../input/input.component';
import { ButtonComponent } from '../../../../button/button.component';
import { HeadComponent } from '../../../head/head.component';
import { AdminPageService, Place } from '../../admin-page.service';

@Component({
  selector: 'app-add-page',
  standalone: true,
  imports: [CommonModule, InputComponent, ButtonComponent, HeadComponent],
  providers:[AdminPageService],
  templateUrl: './edit-place.component.html',
  styleUrl: './edit-place.component.css'
})
export class EditPlaceComponent implements AfterViewInit {

  constructor(private route: ActivatedRoute, private router: Router, private AdminService: AdminPageService) {  }

  @ViewChildren(InputComponent) children!: QueryList<InputComponent>;

  id : number = 0;

  ngAfterViewInit() {

    this.route.params.subscribe(params=>this.id=params["id"]);

    this.route.queryParams.subscribe( params => {
      console.log("dsd "+params['inputValue1']  );
     // this.children.toArray()[0].inputValue = "ASASA";

      if ('inputValue1' in params)
      this.children.toArray()[0].inputValue = params['inputValue1'];
      if ('inputValue2' in params)
      this.children.toArray()[1].inputValue = params['inputValue2'];
      if ('inputValue3' in params)
      this.children.toArray()[2].inputValue = params['inputValue3'];
    });
  }

  updateUrl(val: string) {
    const queryParams = {
      inputValue1: this.children.toArray()[0].inputValue,
      inputValue2: this.children.toArray()[1].inputValue,
      inputValue3: this.children.toArray()[2].inputValue,
    };
    this.router.navigate(['admin/editPlace', this.id], { queryParams });
  }


  EditClick()
  {
    //console.log(this.children.toArray()[2].inputValue);
    this.AdminService.updatePlaceById(this.id, new Place(this.id,
      Number(this.children.toArray()[0].inputValue),
      Number(this.children.toArray()[1].inputValue),
      Number(this.children.toArray()[2].inputValue))).subscribe(
      (error) => {
  console.error('Error fetching users:', error);
}
);
     this.router.navigate(['admin']);
  }

  ExitClick()
  {
  this.router.navigate(['admin']);
  }
}
