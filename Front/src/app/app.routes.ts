import { Routes } from '@angular/router';

const itemRoutes: Routes = [
  { path: 'addUser', loadComponent: () =>
  import('./pages/hradmin-page/additional_pages/add-page/add-page.component').then(m => m.AddPageComponent) },
  { path: 'editUser/:id',loadComponent: () =>
  import('./pages/hradmin-page/additional_pages/edit-page/edit-page.component').then(m => m.EditPageComponent) },
];


export const routes: Routes = [
  { path: 'hradmin',
    loadComponent: () =>
    import('./pages/hradmin-page/hradmin-page.component').then(m => m.HradminPageComponent)},
    {path: 'hradmin/addUser', loadComponent: () =>
    import('./pages/hradmin-page/additional_pages/add-page/add-page.component').then(m => m.AddPageComponent)},
    {path: 'hradmin/editUser/:id', loadComponent: () =>
    import('./pages/hradmin-page/additional_pages/edit-page/edit-page.component').then(m => m.EditPageComponent)},

    {path: 'admin', loadComponent: () =>
    import('./pages/admin-page/admin-page.component').then(m => m.AdminPageComponent)},
    {path: 'admin/editPlace/:id', loadComponent: () =>
    import('./pages/admin-page/additional-pages/edit-place/edit-place.component').then(m => m.EditPlaceComponent)},
    {path: 'admin/addPlace', loadComponent: () =>
    import('./pages/admin-page/additional-pages/add-place/add-place.component').then(m => m.AddPlaceComponent)},
    {path: 'admin/addInv', loadComponent: () =>
    import('./pages/admin-page/additional-pages/add-inventory/add-inventory.component').then(m => m.AddInventoryComponent)},

    { path: 'worker',
    loadComponent: () =>
    import('./pages/worker-page/worker-page.component').then(m => m.WorkerPageComponent)},

    { path: 'warehouseman',
    loadComponent: () =>
    import('./pages/warehouseman-page/warehouseman-page.component').then(m => m.WarehousemanPageComponent)},

    {path: '', loadComponent: () =>
    import('./pages/enter-page/enter-page.component').then(m => m.EnterPageComponent) },
  ];
