import UserTableHeader from '@/app/admin/users/_components/table-header-feature'
import AppUserTable from '@/app/admin/users/_components/table-users'
import AppPagination from '@/components/table-pagination'
import { Card, CardContent, CardFooter, CardHeader, CardTitle } from '@/components/ui/card'
import { Tabs, TabsContent } from '@/components/ui/tabs'
import { SearchUserOption, User } from '@/types/user.types'

type Props = {
  searchParams: SearchUserOption
}

const users: User[] = [
  {
    id: '1',
    firstName: 'John',
    lastName: 'Doe',
    phone: '555-1234',
    address: '123 Main St, Springfield, USA',
    gender: 'Male',
    dob: '1985-01-15',
    salaryByDay: 150,
    isActive: true,
    roleId: 1
  },
  {
    id: '2',
    firstName: 'Jane',
    lastName: 'Smith',
    phone: '555-5678',
    address: '456 Elm St, Springfield, USA',
    gender: 'Female',
    dob: '1990-03-22',
    salaryByDay: 200,
    isActive: true,
    roleId: 2
  },
  {
    id: '3',
    firstName: 'Alice',
    lastName: 'Johnson',
    phone: '555-9101',
    address: '789 Oak St, Springfield, USA',
    gender: 'Female',
    dob: '1988-07-10',
    salaryByDay: 180,
    isActive: false,
    roleId: 3
  },
  {
    id: '4',
    firstName: 'Bob',
    lastName: 'Brown',
    phone: '555-1122',
    address: '101 Pine St, Springfield, USA',
    gender: 'Male',
    dob: '1975-11-30',
    salaryByDay: 220,
    isActive: true,
    roleId: 4
  },
  {
    id: '5',
    firstName: 'Charlie',
    lastName: 'Davis',
    phone: '555-1314',
    address: '202 Maple St, Springfield, USA',
    gender: 'Male',
    dob: '1992-05-18',
    salaryByDay: 190,
    isActive: false,
    roleId: 1
  }
]

export default function UsersPage({ searchParams }: Props) {
  return (
    <Tabs defaultValue='all'>
      <TabsContent value='all'>
        <Card x-chunk='dashboard-06-chunk-0'>
          <CardHeader>
            <CardTitle className='text-center text-xl'>Quản lý nhân viên</CardTitle>
          </CardHeader>
          <CardContent>
            <UserTableHeader searchOptions={searchParams} />
            <AppUserTable data={users} />
          </CardContent>
          <CardFooter>
            <AppPagination currentPage={searchParams.pageIndex ? Number(searchParams.pageIndex) : 1} totalPages={10} />
          </CardFooter>
        </Card>
      </TabsContent>
    </Tabs>
  )
}
