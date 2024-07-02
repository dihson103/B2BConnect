import { Event, SearchEventOption } from '@/types/event.types'
import { Card, CardContent, CardFooter, CardHeader, CardTitle } from '@/components/ui/card'
import { Tabs, TabsContent } from '@/components/ui/tabs'
import AppEventTable from '@/app/admin/events/components/table-events'
import EventTableHeader from '@/app/admin/events/components/table-header-feature'
import AppPagination from '@/components/table-pagination'

type Props = {
  searchParams: SearchEventOption
}

const events: Event[] = [
  {
    id: 1,
    name: 'Sự kiện giao lưu công nghệ',
    startDate: '2024-06-26T14:09:41.139Z',
    endDate: '2024-06-29T14:09:41.139Z',
    numberCompany: '20',
    status: 0,
    statusDescription: 'Sắp diễn ra',
    location: 'Hà Nội',
    image: 'https://via.placeholder.com/150'
  },
  {
    id: 2,
    name: 'Hội nghị thượng đỉnh AI',
    startDate: '2024-07-01T10:00:00.000Z',
    endDate: '2024-07-03T18:00:00.000Z',
    numberCompany: '50',
    status: 1,
    statusDescription: 'Đang diễn ra',
    location: 'TP. Hồ Chí Minh',
    image: 'https://via.placeholder.com/150'
  },
  {
    id: 3,
    name: 'Hội thảo Blockchain',
    startDate: '2024-08-15T08:00:00.000Z',
    endDate: '2024-08-16T17:00:00.000Z',
    numberCompany: '15',
    status: 2,
    statusDescription: 'Đã kết thúc',
    location: 'Đà Nẵng',
    image: 'https://via.placeholder.com/150'
  },
  {
    id: 4,
    name: 'Triển lãm công nghệ xanh',
    startDate: '2024-09-10T09:00:00.000Z',
    endDate: '2024-09-12T18:00:00.000Z',
    numberCompany: '30',
    status: 0,
    statusDescription: 'Sắp diễn ra',
    location: 'Hải Phòng',
    image: 'https://via.placeholder.com/150'
  },
  {
    id: 5,
    name: 'Hội nghị công nghệ thực tế ảo',
    startDate: '2024-10-05T09:00:00.000Z',
    endDate: '2024-10-07T18:00:00.000Z',
    numberCompany: '25',
    status: 1,
    statusDescription: 'Đang diễn ra',
    location: 'Cần Thơ',
    image: 'https://via.placeholder.com/150'
  }
]

export default async function EventPage({ searchParams }: Props) {
  return (
    <Tabs defaultValue='all'>
      <TabsContent value='all'>
        <Card x-chunk='dashboard-06-chunk-0'>
          <CardHeader>
            <CardTitle className='text-center text-xl'>Quản lý sự kiện</CardTitle>
          </CardHeader>
          <CardContent>
            <EventTableHeader searchParams={searchParams} />
            <AppEventTable data={events} />
          </CardContent>
          <CardFooter>
            <AppPagination currentPage={searchParams.pageIndex ? Number(searchParams.pageIndex) : 1} totalPages={10} />
          </CardFooter>
        </Card>
      </TabsContent>
    </Tabs>
  )
}
