export type BreadcrumbType = {
  url: string
  display: string
  value: string
}

export const BreadcrumbValue = new Map<string, BreadcrumbType>([
  [
    '/admin/dashboard',
    {
      url: '/admin/dashboard',
      display: 'Quản lý thống kê',
      value: 'Toàn bộ thống kê'
    }
  ],
  [
    '/admin/users',
    {
      url: '/admin/users',
      display: 'Quản lý nhân viên',
      value: 'Toàn bộ nhân viên'
    }
  ],
  [
    '/admin/events',
    {
      url: '/admin/events',
      display: 'Quản lý sự kiện',
      value: 'Toàn bộ sự kiện'
    }
  ]
])
