import { getEventById } from '@/actions/event.actions'
import UpdateEventPage from '@/app/admin/events/update/_components/update-event-form'
import NotFoundPage from '@/components/not-found'
import { apiErrorHandler } from '@/lib/utils'
import { Card } from '@/components/ui/card'
import { Tabs, TabsContent } from '@/components/ui/tabs'
import { redirect } from 'next/navigation'

export default async function EventDetailUpdatePage({ params }: { params: { id: string } }) {
  try {
    const data = await getEventById(params.id)
    const event = data.data
    const industries = event.industries
    return (
      <Tabs defaultValue='all'>
        <TabsContent value='all'>
          <Card x-chunk='dashboard-06-chunk-0'>
            <UpdateEventPage data={event} industries={industries} />
          </Card>
        </TabsContent>
      </Tabs>
    )
  } catch (error: any) {
    const infor = error.message.split('|')
    const status = infor[0]
    const message = infor[1]

    if (status === '500') {
      return (
        <Tabs defaultValue='all'>
          <TabsContent value='all'>
            <Card x-chunk='dashboard-06-chunk-0'>
              <NotFoundPage />
            </Card>
          </TabsContent>
        </Tabs>
      )
    } else if (status === '400') {
      return <NotFoundPage />
    } else if (status === '401') {
      redirect('/api/auth/test')
    } else {
      apiErrorHandler(message)
    }
  }
}
