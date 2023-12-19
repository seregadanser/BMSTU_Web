import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';



export class MyWorker
{
  Id: number  = 0;
  Login: string = "";
  Name: string= "";
  SecondName: string= "";
  Position: string= "";
  DateOfBirthday: string= "";
  Come: number= 0;
  constructor(id: number, login: string, name: string, secondName: string, position: string, dateOfBirth: string, come: number) {
    this.Id = id;
    this.Login = login;
    this.Name = name;
    this.SecondName = secondName;
    this.Position = position;
    this.DateOfBirthday = dateOfBirth;
    this.Come = come;
  }
}

export class MyWorkerP
{
  Login: string = "";
  Name: string= "";
  SecondName: string= "";
  Position: string= "";
  DateOfBirthday: string= "";
  password: string = "";
  Come: number= 0;
  constructor(login: string, name: string, secondName: string, position: string, dateOfBirth: string, come: number,passwor:string) {
    this.Login = login;
    this.Name = name;
    this.SecondName = secondName;
    this.Position = position;
    this.DateOfBirthday = dateOfBirth;
    this.Come = come;
    this.password = passwor;
  }
}


@Injectable({
  providedIn: 'root'
})
export class HradminPageService {

  private apiUrl = 'http://localhost:5001/persons'

  private columns: string[] = ['Id', 'Login', 'Name', "SecondName", "Position", "DateOfBirthday", "Come"];
  private data: MyWorker[] = [
    { "Name": 'Alice', Id:1, Login:"sss", SecondName:"", Position:"", DateOfBirthday:"", Come:0},
  ];

  private page : number = 1;
  private perPage: number = 2;

  constructor(private http: HttpClient) { }



  getColumns(): string[]
  {
    return this.columns;
  }

  getUsers(): Observable<{items: MyWorker[]; pages: number}>
  {
    const params = new HttpParams()
    .set('page', this.page ? this.page.toString() : '')
    .set('per_page', this.perPage ? this.perPage.toString() : '');

    const headers = new HttpHeaders({ "Authorization": "Bearer " +  localStorage.getItem("id_token") });

    return this.http.get(`${this.apiUrl}`, { headers , params }).pipe(
      map((data: any) => ({
        items: data.results.map((workerData: any )=> new MyWorker(
          workerData.id,
          workerData.login,
          workerData.name,
          workerData.secondName,
          workerData.position,
          workerData.dateOfBirthday,
          workerData.numberOfCome
        )),
        pages: Math.ceil(data.total/data.per_page),
      }))
    );
  }

  addUser(person: MyWorkerP): Observable<any>
  {
    const headers = new HttpHeaders({ "Authorization": "Bearer " +  localStorage.getItem("id_token") });
    return this.http.post(`${this.apiUrl}`, person, {headers});
  }

  editUser(id:number,person: MyWorker): Observable<any>
  {
    const headers = new HttpHeaders({ "Authorization": "Bearer " +  localStorage.getItem("id_token") });
    return this.http.patch(`${this.apiUrl}/${id}`, person, {headers});
  }

  deleteUser(id: number): any
  {
    const headers = new HttpHeaders({ "Authorization": "Bearer " +  localStorage.getItem("id_token") });
    return this.http.delete(`${this.apiUrl}/${id}`, {headers});
  }

  searchUser(value: string): any
  {
    const headers = new HttpHeaders({ "Authorization": "Bearer " +  localStorage.getItem("id_token") });
    return null;
  }

  onGoTo(page: number): number {
    this.page = page;
    return this.page;
  }

  onNext(): number {
    this.page = this.page + 1;
    return this.page;
  }

  onPrevious(): number {
    this.page = this.page - 1;
    return this.page;
  }
}
